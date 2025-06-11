/*using System;
using System.Threading.Tasks;*/
using System.Text.Json;
using TodoTasks;

//File that keeps the data from the objects
var fileName = "ToDoList.json";

//List that the program interacts with
List<TodoTask> taskList = new List<TodoTask>();

//Reads saved data from JSON to tasklist
await ReadDataJSON();


void CreateTask()
{
  //Basic information needed for task creation
  Console.WriteLine("What type of task would you like to create?");
  string title = Console.ReadLine() ?? "";
  Console.WriteLine("Could you provide a project?");
  string project = Console.ReadLine() ?? "";

  //Check for due date (information not needed)
  Console.WriteLine("Would you like to set a due date? (y/n)");
  string dateDueChoice = Console.ReadLine() ?? "";
  if (dateDueChoice == "y")
  {
    Console.WriteLine("What year would you like to have the task completed?");
    string dateDueYear = Console.ReadLine() ?? "";
    Console.WriteLine("What month would you like to have the task completed? (1-12)");
    string dateDueMonth = Console.ReadLine() ?? "";
    Console.WriteLine("What day would you like to have the task completed? (1-31)");
    string dateDueDay = Console.ReadLine() ?? "";
    DateTime dateDue = new DateTime(Int32.Parse(dateDueYear), Int32.Parse(dateDueMonth), Int32.Parse(dateDueDay));

    //add task to list
    taskList.Add(new TodoTask(title, project, dateDue));
  }
  else
  {
    //add task to list
    taskList.Add(new TodoTask(title, project));
  }
}

void ListByDate()
{
  //sort by due date
  var listByDate = taskList.OrderBy(t => t.DateDue).ToList();

  //list the titles for the list:
  listByDate[0].GetInfoTitles();

  //loop through listBydate
  foreach (TodoTask task in listByDate)
  {
    task.GetInfo();
  }
  Console.WriteLine("");
}
void ListByProject()
{
  //sort by project
  var listByProject = taskList.OrderBy(t => t.Project).ToList();
  //list the titles for the list:
  listByProject[0].GetInfoTitles();
  //loop through listByProject
  foreach (TodoTask task in listByProject)
  {
    task.GetInfo();
  }
  Console.WriteLine("");
}

void EditTask()
{
  //Select which task
  int counter = 0;
  foreach (TodoTask task in taskList)
  {

    Console.WriteLine($"{counter}. {task.Title}");
    counter++;
  }
  Console.WriteLine("Which task would you like to edit");
  //contains the index selection for the list later
  string taskChoice = Console.ReadLine() ?? "";

  //What would you like to do to the task
  Console.WriteLine("What would you like to do with the task?\n1. Update Task\n2. Mark as done\n3. Remove task");
  string taskEditChoice = Console.ReadLine() ?? "";
  switch (taskEditChoice)
  {
    case "1":
      Console.WriteLine("Which part would you like to edit?\n1. Title\n2. project\n3. Due Date");
      string editChoice = Console.ReadLine() ?? "";
      switch (editChoice)
      {
        case "1":
          Console.WriteLine("What would you like your new title to be?");
          string newTitle = Console.ReadLine() ?? "";
          taskList[int.Parse(taskChoice)].Title = newTitle;
          break;
        case "2":
          Console.WriteLine("What would you like your new project to be?");
          string newProject = Console.ReadLine() ?? "";
          taskList[int.Parse(taskChoice)].Project = newProject;
          break;
        case "3":
          Console.WriteLine("What would you like your new due date to be? (yyyy-MM-dd)");
          string newDueDate = Console.ReadLine() ?? "";
          string[] newDueDateSplit = newDueDate.Split("-");
          taskList[int.Parse(taskChoice)].DateDue = new DateTime(int.Parse(newDueDateSplit[0]), int.Parse(newDueDateSplit[1]), int.Parse(newDueDateSplit[2]));
          break;
      }
      break;
    case "2":
      taskList[int.Parse(taskChoice)].SetComplete();
      break;
    case "3":
      taskList.RemoveAt(int.Parse(taskChoice));
      break;
  }
}

async Task AddDataJSON()
{
  //Open stream to file allowing input
  using FileStream createStream = File.Create(fileName);
  //Write entire content of list to file in JSON
  await JsonSerializer.SerializeAsync(createStream, taskList);
  //Close the stream
  await createStream.DisposeAsync();
}

async Task ReadDataJSON()
{
  //Open stream to file Read Only
  using FileStream openStream = File.OpenRead(fileName);
  // Deserialize JSON-array to a list of TodoTask
  List<TodoTask>? tasks = await JsonSerializer.DeserializeAsync<List<TodoTask>>(openStream);
  //Loop through list and add TodoTask object to tasklist
  if (tasks != null)
  {
    foreach (var task in tasks)
    {
      taskList.Add(task);
    }
  }
  //close stream
  await openStream.DisposeAsync();
}

//Program Loop
bool running = true;
Console.WriteLine("This is your todolist - choose an option to continue:");
while (running)
{
  Console.WriteLine("1. Show tasks\n2. Create a new task\n3. Edit Task\n4. Save and exit");
  string optionSelection = Console.ReadLine() ?? "";
  switch (optionSelection)
  {
    case "1":
      Console.WriteLine("What would you like to list by?\n1. Due Date\n2. Project");
      string listSelect = Console.ReadLine() ?? "";
      if (listSelect == "1")
      {
        ListByDate();
      }
      else
      {
        ListByProject();
      }
      break;
    case "2":
      CreateTask();
      break;
    case "3":
      EditTask();
      break;
    case "4":
      await AddDataJSON();
      running = false;
      break;
  }
}

