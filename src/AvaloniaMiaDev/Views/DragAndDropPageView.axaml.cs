using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using AvaloniaMiaDev.ViewModels.SplitViewPane;

namespace AvaloniaMiaDev.Views;

public partial class DragAndDropPageView : UserControl
{
    private Point _ghostPosition = new(0,0);
    private readonly Point _mouseOffset = new(-5, -5);

    public DragAndDropPageView()
    {
        InitializeComponent();

        AddHandler(DragDrop.DragOverEvent, DragOver);
        AddHandler(DragDrop.DropEvent, Drop);
    }

    // OnInitialized didn't work for some reason
    protected override void OnLoaded(RoutedEventArgs e)
    {
        GhostItem.IsVisible = false;
        base.OnLoaded(e);
    }

    private async void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        Console.WriteLine("DoDrag start");

        if (sender is not Border border) return;
        if (border.DataContext is not TaskItem taskItem) return;

        var ghostPos = GhostItem.Bounds.Position;
        _ghostPosition = new Point(ghostPos.X + _mouseOffset.X, ghostPos.Y + _mouseOffset.Y);

        var mousePos = e.GetPosition(MainContainer);
        var offsetX = mousePos.X - ghostPos.X;
        var offsetY = mousePos.Y - ghostPos.Y + _mouseOffset.X;
        GhostItem.RenderTransform = new TranslateTransform(offsetX, offsetY);

        if (DataContext is not DragAndDropPageViewModel vm) return;
        vm.StartDrag(taskItem);

        GhostItem.IsVisible = true;

        var dragData = new DataObject();
        dragData.Set(DragAndDropPageViewModel.CustomFormat, taskItem);
        var result = await DragDrop.DoDragDrop(e, dragData, DragDropEffects.Move);
        Console.WriteLine($"DragAndDrop result: {result}");
        GhostItem.IsVisible = false;
    }

    private void DragOver(object? sender, DragEventArgs e)
    {
        var currentPosition = e.GetPosition(MainContainer);

        var offsetX = currentPosition.X - _ghostPosition.X;
        var offsetY = currentPosition.Y - _ghostPosition.Y;

        GhostItem.RenderTransform = new TranslateTransform(offsetX, offsetY);

        // set drag cursor icon
        e.DragEffects = DragDropEffects.Move;
        if (DataContext is not DragAndDropPageViewModel vm) return;
        var data = e.Data.Get(DragAndDropPageViewModel.CustomFormat);
        if (data is not TaskItem taskItem) return;
        if (!vm.IsDestinationValid(taskItem, (e.Source as Control)?.Name))
        {
            e.DragEffects = DragDropEffects.None;
        }
    }

    private void Drop(object? sender, DragEventArgs e)
    {
        Console.WriteLine("Drop");

        var data = e.Data.Get(DragAndDropPageViewModel.CustomFormat);

        if (data is not TaskItem taskItem)
        {
            Console.WriteLine("No task item");
            return;
        }

        if (DataContext is not DragAndDropPageViewModel vm) return;
        vm.Drop(taskItem, (e.Source as Control)?.Name);
    }
}
