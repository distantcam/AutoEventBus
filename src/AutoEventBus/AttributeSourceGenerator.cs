using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis;
using System.Text;

namespace AutoEventBus;

[Generator(LanguageNames.CSharp)]
public class AttributeSourceGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var source = new CodeBuilder();
        source.AppendHeader();
        source.AppendLine();
        source.AppendPragma("#if AUTOEVENTBUS_EMBED_ATTRIBUTES");
        source.AppendLine("namespace AutoEventBus");
        source.StartBlock();
        source.AppendLine("[System.Runtime.CompilerServices.CompilerGenerated]");
        source.AppendLine("[System.AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]");
        source.AppendLine("[System.Diagnostics.Conditional(\"\"AUTOEVENTBUS_USAGES\"\")]");
        source.AppendLine("internal sealed class SubscriberAttribute : System.Attribute");
        source.StartBlock();
        source.AppendLine("[System.Runtime.CompilerServices.CompilerGenerated]");
        source.AppendLine("public AutoConstructAttribute()");
        source.StartBlock();
        source.EndBlock();
        source.EndBlock();
        source.EndBlock();
        source.AppendPragma("#endif");

        context.AddSource("AutoEventBusAttribute.g.cs", source);
    }
}
