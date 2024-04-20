using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using AvaloniaMiaDev.Helpers;

namespace AvaloniaMiaDev.ViewModels;

public class ImagePageViewModel : ViewModelBase, ISplitViewIcon
{
    public string IconName => "ImageRegular";
    public string ImageSourceString => "/Assets/Images/snow.jpg";
    public Bitmap ImageSourceBitmapLocal
        => ImageHelper.LoadFromResource("/Assets/Images/tiny_house.jpg");
    public Task<Bitmap?> ImageSourceBitmapWeb
        => ImageHelper.LoadFromWeb("https://images.unsplash.com/photo-1607956853617-d9d248a8f327?q=80&w=600&auto=format&fit=crop");
}
