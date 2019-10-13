namespace Converter
{
    public interface IConverter
    {
        Result Convert();

        Result Convert(string text2Convert);
    }
}
