namespace Annex.Core.Platforms;

public interface IPlatformScreenService
{
    (int Width, int Height) GetPrimaryScreenSize();
    (int Width, int Height) GetPrimaryScreenContentSize();
}
