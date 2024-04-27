using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaMiaDev.ViewModels.SplitViewPane;

public partial class DragAndDropPageViewModel : ViewModelBase
{
    public const string CustomFormat = "task-item-format";
    private int _count;

    [ObservableProperty] private ObservableCollection<TaskItem> _todoTasks = [
        new TaskItem("TicketStatic0", "Clean"),
        new TaskItem("TicketStatic1", "Gifts"),
    ];

    [ObservableProperty] private ObservableCollection<TaskItem> _doneTasks = [];
    [ObservableProperty] private TaskItem? _draggingTaskItem;

    [RelayCommand]
    private void AddTask()
    {
        var id = $"Task{++_count}";
        TodoTasks.Add(new TaskItem(id, id));
    }

    public void StartDrag(TaskItem taskItem)
    {
        DraggingTaskItem = taskItem;
    }

    public void Drop(TaskItem taskItem, string? destinationListName)
    {
        var sourceList = GetSourceList(taskItem.Status);
        var item = sourceList.SingleOrDefault(t => t.TicketId == taskItem.TicketId);
        if (item is null)
        {
            Console.WriteLine($"Task with id '{taskItem.TicketId}' not found");
            return;
        }

        var destination = GetDestinationList(taskItem.Status);

        if (destination.ListName != destinationListName)
        {
            Console.WriteLine($"Invalid drop location '{destinationListName}'. Valid location is {destination.ListName}");
            return;
        }

        sourceList.Remove(item);
        var updatedItem = item.UpdateStatus(destination.Status);
        destination.List.Add(updatedItem);
        Console.WriteLine($"Moving task '{taskItem.TicketId}' from '{item.Status}' to '{updatedItem.Status}'");
    }

    public bool IsDestinationValid(TaskItem taskItem, string? destinationName)
    {
        var destination = GetDestinationList(taskItem.Status);
        return destination.ListName == destinationName;
    }

    private ObservableCollection<TaskItem> GetSourceList(string status)
    {
        return status switch
        {
            "todo" => TodoTasks,
            "done" => DoneTasks,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }

    private (ObservableCollection<TaskItem> List, string ListName, string Status) GetDestinationList(string status)
    {
        return status switch
        {
            "todo" => (DoneTasks, "DoneItems", "done"),
            "done" => (TodoTasks, "TodoItems", "todo"),
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
}

public record TaskItem(string TicketId, string Title, string Status = "todo")
{
    public TaskItem UpdateStatus(string newStatus) => this with { Status = newStatus };
}
