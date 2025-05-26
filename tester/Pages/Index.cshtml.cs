using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace tester.Pages
{
    public class IndexModel : PageModel
    {
        private const string TodoSessionKey = "TodoItems";

        public List<string> itemList = new List<string>();

        [BindProperty]
        public string newTask { get; set; }

        public void OnGet()
        {
            

            var taskJson = HttpContext.Session.GetString(TodoSessionKey);

            if (!string.IsNullOrEmpty(taskJson))
            {
                itemList = JsonSerializer.Deserialize<List<string>>(taskJson);
            }
            else
            {
                // If no tasks exist in session, we initialize an empty list
                itemList = new List<string>();
            }
        }

        public void OnPost() {

            if (!string.IsNullOrEmpty(newTask))
            {

                // If tasks already exist in session, load the current tasks

                if (itemList.Count == 0)
                {
                    var taskJsons = HttpContext.Session.GetString(TodoSessionKey);

                    if (!string.IsNullOrEmpty(taskJsons))
                    {
                        itemList = JsonSerializer.Deserialize<List<string>>(taskJsons);
                    }
                }

                // Add new task to the list
                itemList.Add(newTask);

                // Serialize the updated list and save it back to session
                var taskJson = JsonSerializer.Serialize(itemList);
                HttpContext.Session.SetString(TodoSessionKey, taskJson);

            }
        }
        
    }
}
