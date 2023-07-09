using System.Collections.Immutable;
using AutoSource;
using Microsoft.CodeAnalysis;

namespace AutoEventBus;

[Generator(LanguageNames.CSharp)]
public class BusSourceGenerator : IIncrementalGenerator
{
    private static readonly SymbolDisplayFormat HintSymbolDisplayFormat = new SymbolDisplayFormat(
        globalNamespaceStyle:
            SymbolDisplayGlobalNamespaceStyle.Omitted,
        typeQualificationStyle:
            SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
        genericsOptions:
            SymbolDisplayGenericsOptions.IncludeTypeParameters,
        miscellaneousOptions:
            SymbolDisplayMiscellaneousOptions.UseSpecialTypes);

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var methods = context.SyntaxProvider.CreateSyntaxProvider(
            static (s, ct) => SourceTools.IsCorrectAttribute("Subscriber", s, ct),
            SourceTools.GetMethodFromAttribute)
            .Where(x => x != null)
            .Collect();

        context.RegisterSourceOutput(methods, GenerateSource);
    }

    private static void GenerateSource(SourceProductionContext context, ImmutableArray<IMethodSymbol?> methods)
    {
        if (methods.IsDefaultOrEmpty)
            return;

        var types = methods.Select(m => m?.ContainingType).Where(t => t is not null).Distinct(SymbolEqualityComparer.Default);

        foreach (var type in types)
        {
            var typeString = type!.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            var subListName = "_subList_" + type.Name;

            var source = new CodeBuilder();
            source.AppendHeader();
            source.AppendLine();
            source.AppendLine("using System.Collections.Generic;");
            source.AppendLine();

            source.AppendLine("public static partial class EventBus");
            source.StartBlock();

            source.AppendLine($"private static readonly List<{typeString}> {subListName} = new List<{typeString}>(8);");
            source.AppendLine();

            source.AppendLine($"public static void Subscribe({typeString} subscriber)");
            source.StartBlock();
            source.AppendLine($"lock ({subListName})");
            source.StartBlock();
            source.AppendLine($"{subListName}.Add(subscriber);");
            source.EndBlock(); // lock
            source.EndBlock(); // Subscribe

            source.AppendLine($"public static void Unsubscribe({typeString} subscriber)");
            source.StartBlock();
            source.AppendLine($"lock ({subListName})");
            source.StartBlock();
            source.AppendLine($"{subListName}.Remove(subscriber);");
            source.EndBlock(); // lock
            source.EndBlock(); // Unsubscribe

            source.EndBlock(); // EventBus

            var hintName = type.ToDisplayString(HintSymbolDisplayFormat)
                .Replace('<', '[')
                .Replace('>', ']');

            context.AddSource($"EventBus_{hintName}.g.cs", source);
        }

        foreach (var methodGroup in methods.GroupBy(m => m?.Parameters.JoinString(format: SourceDisplayFormats.FullyQualifiedParameterFormat)))
        {
            if (methodGroup == null || methodGroup.Key == null || !methodGroup.Any())
                continue;

            var source = new CodeBuilder();
            source.AppendHeader();
            source.AppendLine();

            source.AppendLine("public static partial class EventBus");
            source.StartBlock();

            var parameters = methodGroup.First()!.Parameters;

            source.AppendLine($"public static void Send({parameters.JoinString(format: SourceDisplayFormats.FullyQualifiedParameterFormat)})");
            source.StartBlock();
            foreach (var methodTypeGroup in methodGroup.GroupBy(m => "_subList_" + m!.ContainingType.Name))
            {
                //var subListName = "_sub" + method!.ContainingType.Name;

                source.AppendLine($"lock ({methodTypeGroup.Key})");
                source.StartBlock();
                source.AppendLineNoIndent("#if NET5_0_OR_GREATER");
                source.AppendLine($"foreach (var receiver in System.Runtime.InteropServices.CollectionsMarshal.AsSpan({methodTypeGroup.Key}))");
                source.AppendLineNoIndent("#else");
                source.AppendLine($"foreach (var receiver in {methodTypeGroup.Key})");
                source.AppendLineNoIndent("#endif");
                source.StartBlock();
                foreach (var method in methodTypeGroup)
                {
                    source.AppendLine($"receiver.{method!.Name}({method.Parameters.Select(p => p.RefKind.ToParameterPrefix() + p.Name).JoinString()});");
                }
                source.EndBlock(); // foreach
                source.EndBlock(); // lock
            }
            source.EndBlock(); // Send
            source.EndBlock(); // EventBus

            var hintName = parameters.Select(p => p.RefKind.ToParameterPrefix() + "_" + p.Type.Name)
                .JoinString("_")
                .Replace('<', '[')
                .Replace('>', ']');
            context.AddSource($"EventBus_Send_{hintName}.g.cs", source);
        }
    }
}
