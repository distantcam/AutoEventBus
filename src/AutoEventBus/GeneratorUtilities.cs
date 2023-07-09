using Microsoft.CodeAnalysis;

internal static class GeneratorUtilities
{
    public static string ToParameterPrefix(this RefKind kind)
    {
        return kind switch
        {
            RefKind.Ref => "ref ",
            RefKind.Out => "out ",
            RefKind.In => "in ",
            _ => string.Empty
        };
    }

    public static string JoinString<T>(this IEnumerable<T> items, string separator = ", ") =>
        string.Join(separator, items);

    public static string JoinString(this IEnumerable<ISymbol> items, string separator = ", ", SymbolDisplayFormat? format = null) =>
        string.Join(separator, items.Select(s => s.ToDisplayString(format)));

}
