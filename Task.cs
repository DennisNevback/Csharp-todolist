using System;
using System.Reflection;

namespace TodoTasks
{
  class TodoTask
  {
    // properties
    public string Title { get; set; }
    public string Project { get; set; }
    public DateTime? DateDue { get; set; }
    public DateTime? DateCompleted { get; set; }
    public DateTime DateAdded { get; set; }
    public bool Completed { get; set; }

    // constructor
    public TodoTask(string title, string project, DateTime? dateDue = null)
    {
      this.Title = title;
      this.Project = project;
      this.DateDue = dateDue;
      this.DateAdded = DateTime.Now;
      this.Completed = false;
    }


    public void SetDateDue(int year, int month, int day)
    {
      DateTime date_due = new DateTime(year, month, day);
      Console.WriteLine(date_due);
      this.DateDue = date_due;
    }

    public void SetComplete()
    {
      this.Completed = true;
      this.DateCompleted = DateTime.Now;
    }

    public void UndoComplete()
    {
      this.Completed = false;
      this.DateCompleted = null;
    }

    public void GetInfoTitles()
    {
      foreach (PropertyInfo prop in this.GetType().GetProperties())
      {
        var value = prop.GetValue(this);
        Console.Write($"{prop.Name.ToUpper(),-30}");
      }
      Console.WriteLine("");
    }
    public void GetInfo()
    {
      foreach (PropertyInfo prop in this.GetType().GetProperties())
      {
        var value = prop.GetValue(this);
        Console.Write($"{value,-30}");
      }
      Console.WriteLine("");
    }
  }
}
