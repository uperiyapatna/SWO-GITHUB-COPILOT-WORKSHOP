# Lab 2 - Unit Tests

In this lab, participants explore coding exercises with GitHub Copilot to grasp its suggestions and features. The focus is on running and incorporating unit tests, emphasizing pair programming. The lab follows a step-by-step structure: beginning with executing existing unit tests, then enhancing test coverage, and addressing specific functionalities such as case sensitivity and trimming in search methods.

> [!IMPORTANT]
> Not every test is successful every time. You will need to debug the tests and fix the problems if they fail. While Copilot and other similar tools can be a big help, it is up to you as the Pilot to figure out what's wrong and how to fix it.

## Time Required

- 20 minutes

## Goals

- Engaging in straightforward coding exercises with GitHub Copilot to better understand its suggestions and capabilities.
- Practicing pair programming, where one person writes the code (the ‘pilot’) while the other provides guidance based on Copilot’s suggestions

### Step 1: Run existing unit tests

- Open GitHub Copilot Chat and click the **+** button to clear the prompt history.

- Type the following in the chat window:

    ```text
    @workspace how do I run the unit tests?
    ```

- Copilot will suggest running unit tests in the terminal.

    ```sh
    cd src/BikeShopAPI.Test
    dotnet test
    ```

- To verify everything is functioning correctly, let’s run the unit tests.

- From the Copilot Chat window, choose one of these options:
    1. Click the ellipses, `...` and select `Insert into Terminal`.

    2. If there’s no open terminal, click the `Open in Terminal` button.

    3. Alternatively, copy the provided command and open a new Terminal window using **Ctrl+`**, then paste the command.

    4. Before running the tests, make sure you don't have a server running the application in another terminal window by `dotnet run`

- Open the terminal and execute the tests using the provided command.

    ```sh
    cd src/BikeShopAPI.Test
    dotnet test
    ```

- The tests should run and pass.

    ```sh
    Starting test execution, please wait...
    A total of 1 test files matched the specified pattern.
    Passed!  - Failed:     0, Passed:     2, Skipped:     0, Total:     2
    ```

    Number of tests can be diffrent from above example.

### Step 2: Completing Unit Tests

- Open GitHub Copilot Chat and click the **+** button to clear the prompt history.

- Type the following in the chat window:

    ```text
    @workspace where do I add additional unit tests?
    ```

- Copilot will give a suggestion to add unit tests to the `Controllers/BikeShopControllerTest.cs` file in the `BikeShopAPI.Tests` project.

    ```text
    You can add additional unit tests in the `BikeShopControllerTest` class in the `BikeShopAPI.Tests/Controllers/BikeShopControllerTest.cs` file.
    ```

- Open the `BikeShopControllerTest.cs` file that GitHub Copilot suggested by clicking on the provided file name in the chat.
- Ensure that you also have the `BikeShopController.cs` file open in your Visual Studio Code Editor, ideally in a tab adjacent to the `BikeShopControllerTest.cs` file. 🚀

    The `BikeShopController.cs` file must be open in order for Github Copilot to use it to obtain additional context for its recommendations.

- Place your cursor at the end of the file, after the `}` of the `GetById_ReturnsBikeShop` method.

```csharp
public class BikeShopControllerTest
{
    [Fact]
    public void GetById_ReturnsBikeShop()
    {
        // body
    }

    <---- Place your cursor here
}
```

- When you hit `Enter`, GitHub Copilot will now, using the code in the `BikeShopController.cs` file, suggest `[Fact]` for a unit test that is missing.

- To accept the suggestion, press `Tab`; to add a new line, press `Enter`.

- A missing unit test will be automatically suggested by GitHub Copilot. Accept the suggestion by pressing `Tab`.

- Now let's add a few more unit tests to the `BikeShopControllerTest.cs` file.

- You can repeat the process to add more unit tests to the `BikeShopControllerTest.cs` file.

- Let's now verify that everything is operating as it should by running the unit tests in the terminal.

- Use the given command to launch the terminal and carry out the tests.

    ```sh
    cd src/BikeShopAPI.Test
    dotnet test
    ```

    Certain tests may not pass. Copilot doesn't always offer the best recommendations. To ensure that the tests are accurate, it's critical to comprehend the recommendations and put in a little more effort. Copilot is also able to assist you with that.

- The tests should run and pass.

    ```sh
    Starting test execution, please wait...
    A total of 1 test files matched the specified pattern.
    Passed!  - Failed:  0, Passed:  3, Skipped:  0, Total:  3
    ```

### Step 3: Developing Robust Tests

- Open the `BikeShopController.cs` file.

- Make sure to add the `SearchByName` method to the `BikeShopController.cs` file if you haven't already in the previous lab.

- Make sure to have the `BikeShopController.cs` file open as well in your Visual Studio Code Editor in a tab next to the `BikeShopControllerTest.cs` file.

- Open `BikeShopControllerTest.cs` file

- Place your cursor at the end of the file, after the last unit test.

    ```csharp
    public class BikeShopControllerTest
    {
        // class body

        <---- Place your cursor here
    }
    ```

- Now, copy and paste this:

    ```csharp
    // Search by name term using SearchByName | Amount of results | Test Description
    // Fast Wheels                            | 1                 | Specific search
    // Wheels                                 | 1                 | General search
    // Fast wheels                            | 1                 | Case insensitive
    // Fast Wheels                            | 1                 | Extra spaces at end
    // Fast  Wheels                           | 1                 | Double space
    ```

- Press `Enter` twice, and GitHub Copilot will automatically propose the `[Theory]` attribute. Accept the suggestion by pressing `Tab`. If Copilot suggests the next comment, then press `Enter` once more.

    Based on the remarks that come before the method, GitHub Copilot will automatically suggest the `[Theory]` attribute. It understands that you want to run the same test with different results and parameters.

- Press `Enter`, GitHub Copilot will automatically suggest the `[InlineData]` attributes. Accept the suggestion by pressing `Enter`. Repeat this for each `[InlineData]` attribute.

    ```csharp
    [Theory]
    // inline data...
    public void SearchByName_ReturnsItems(string name, int expectedAmount)
    {
        // method body...
    }  
    ```

- Let's run the unit tests in the terminal to make sure everything is working as it should.

- Open the terminal and run the tests with the provided command.

    ```sh
    cd src/BikeShopAPI.Test
    dotnet test
    ```

- Not every test will be successful. The tests for `Case insensitive` and `Extra spaces`, for instance, won't work. This is a result of the case sensitivity of the `SearchByName` method. Let's put this right.

    It's possible that when the method was created, Copilot made it insensitive to case. You can then proceed to the next task, as some test cases will still fail after that.

    ```text
    Starting test execution, please wait...
    A total of 1 test files matched the specified pattern.
    Failed!  - Failed:     2, Passed:     6, Skipped:     0, Total:     8
    ```

- Open `BikeShopController.cs` file.

- Select the entire `SearchByName` method.

- To ask Copilot to fix the case sensitivity issue, type the following in the chat window:

    ```text
    @workspace /fix case sensitivity issue
    ```

- Copilot will provide a solution for the case sensitivity issue.

    ```csharp
    public class BikeShopController : ControllerBase
    {
        /* Rest of the methods */
        
        [HttpGet("search")]
        public ActionResult<List<BikeShop>> SearchByName([FromQuery] string name)
        {

            // Rest of the method

            var bikeShops = _controller.FindAll(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)); // <---- Case insensitive

            if (bikeShops == null)
            {
                return NotFound();
            }
            
            return Ok(bikeShops);
        }
    }
    ```

    You'll need to use the `StringComparer.OrdinalIgnoreCase` comparer in the FindAll method.

- Apply the changes to the `BikeShopController.cs` file.

- Click on the `Insert at cursor` to replace the `SearchByName` method with the new one.

- It will also fail the `Extra spaces` test. The reason for this is that the search term is not being trimmed by `SearchByName`. Let's put this right.

- Open `BikeShopController.cs` file

- Select the content of the `SearchByName` method.

- Enter the following in the chat window to request that Copilot resolve the trimming issue:

    ```text
    @workspace /fix trimming issue
    ```

- Copilot will offer a solution to address the trimming problem.

    ```csharp
    public class BikeShopController : ControllerBase
    {
        /* Rest of the methods */
        
        [HttpGet("search")]
        public ActionResult<List<BikeShop>> SearchByName([FromQuery] string name)
        {

            // Rest of the method

            name = name.Trim(); // <---- Removes leading and trailing spaces

            var bikeShops = _controller.FindAll(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

            if (bikeShops == null)
            {
                return NotFound();
            }
            
            return Ok(bikeShops);
        }
    }
    ```

- Apply the changes to the `BikeShopController.cs` file.

- Click on the `Insert at cursor` to replace the `SearchByName` method with the new one.

- Let's run the unit tests in the terminal to make sure everything is working as expected.

- Open the terminal and run the tests with the provided command

    ```sh
    cd src/BikeShopAPI.Test
    dotnet test
    ```

- The tests should run and pass.

    ```sh
    Starting test execution, please wait...
    A total of 1 test files matched the specified pattern.
    Passed!  - Failed:     0, Passed:     8, , Failed:     0
    ```

    It's likely that you also need to address the use case of double spaces in the search parameter if the tests continue to fail. Try to accept Copilot's fix suggestion and put it into practice.

### Step 4 - Creating the new Controller

- Open the `BikeShopAPI` project in Visual Studio Code.

- Open the `Entities/Bike.cs` file.

- Open GitHub Copilot Chat and click the **+** button to clear the prompt history.

- Ask the following question:

    ```text
    @workspace create an ApiController with all the CRUD operations using the Bike class, then add test data for the first three bikes directly in this controller.
    ```

- Copilot will give a suggestion to create an `BikeController` class based on the `Bike` class.

    ```csharp
    using BikeShopAPI.Entities;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    namespace BikeShopAPI.Controllers
    {
        [Route("api/bike")]
        [ApiController]
        public class BikeController : ControllerBase
        {
            private static List<Bike> _bikes = new List<Bike>
            {
                new Bike { Id = 1, Brand = "Brand1", Model = "Model1", Description = "Description1", Price = 1000, ForkTravel = 120, RearTravel = 120, ShopId = 1 },
                new Bike { Id = 2, Brand = "Brand2", Model = "Model2", Description = "Description2", Price = 2000, ForkTravel = 130, RearTravel = 130, ShopId = 2 },
                new Bike { Id = 3, Brand = "Brand3", Model = "Model3", Description = "Description3", Price = 3000, ForkTravel = 140, RearTravel = 140, ShopId = 3 },
            };

            [HttpGet]
            public ActionResult<IEnumerable<Bike>> GetAll()
            {
                return _bikes;
            }

            [HttpGet("{id}")]
            public ActionResult<Bike> GetById(int id)
            {
                var bike = _bikes.FirstOrDefault(b => b.Id == id);
                if (bike == null)
                {
                    return NotFound();
                }
                return bike;
            }

            [HttpPost]
            public ActionResult<Bike> Create(Bike bike)
            {
                _bikes.Add(bike);
                return CreatedAtAction(nameof(GetById), new { id = bike.Id }, bike);
            }

            [HttpPut("{id}")]
            public IActionResult Update(int id, Bike bike)
            {
                var existingBike = _bikes.FirstOrDefault(b => b.Id == id);
                if (existingBike == null)
                {
                    return NotFound();
                }
                existingBike.Brand = bike.Brand;
                existingBike.Model = bike.Model;
                existingBike.Description = bike.Description;
                existingBike.Price = bike.Price;
                existingBike.ForkTravel = bike.ForkTravel;
                existingBike.RearTravel = bike.RearTravel;
                existingBike.ShopId = bike.ShopId;

                return NoContent();
            }

            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                var bike = _bikes.FirstOrDefault(b => b.Id == id);
                if (bike == null)
                {
                    return NotFound();
                }
                _bikes.Remove(bike);
                return NoContent();
            }
        }
    }
    ```

- In GitHub Copilot Chat, click the ellipses `...` and select `Insert into New File` for the suggested `BikeController`.

- Copilot will add the code to a new empty file, but must be saved.

- Save the file by clicking pressing `Ctrl + S` or `Cmd + S`.

- Change directory to the `Controllers` folder`.

- Enter the file name `BikeController.cs` and click `Save`.

- It's time to make sure the `BikeController` is functioning as intended now that you have created it with CRUD operations. You will write unit tests for the `BikeController` in this step.

- Open the `BikeController.cs` file.

- Open GitHub Copilot Chat and click the **+** button to clear the prompt history.

- Ask the following question:

    ```text
    @workspace create all the unit tests for this BikeController
    ```

- Copilot will give a suggestion to create an `BikeControllerTests` class based on the `BikeController` class with all the needed unit tests.

    ```csharp
    using Xunit;
    using Microsoft.AspNetCore.Mvc;
    using BikeShopAPI.Controllers;
    using BikeShopAPI.Entities;
    using System.Collections.Generic;
    using System.Linq;

    namespace BikeShopAPI.Test.Controllers
    {
        public class BikeControllerTest
        {
            private readonly BikeController _controller;

            public BikeControllerTest()
            {
                _controller = new BikeController();
            }

            [Fact]
            public void GetAll_ReturnsAllBikes()
            {
                var result = _controller.GetAll().Value;

                Assert.IsType<List<Bike>>(result);
                Assert.NotEmpty(result);
            }

            [Fact]
            public void GetById_ReturnsBike()
            {
                var result = _controller.GetById(1).Value;

                Assert.IsType<Bike>(result);
                Assert.Equal(1, result.Id);
            }

            [Fact]
            public void GetById_ReturnsNotFound()
            {
                var result = _controller.GetById(999);

                Assert.IsType<NotFoundResult>(result.Result);
            }

            [Fact]
            public void Post_CreatesNewBike()
            {
                var newBike = new Bike
                {
                    Id = 4,
                    Brand = "Brand4",
                    Model = "Model4",
                    Description = "Description4",
                    Price = 4000,
                    ForkTravel = 150,
                    RearTravel = 150,
                    ShopId = 4
                };

                var result = _controller.Post(newBike).Result as CreatedAtActionResult;

                Assert.Equal(201, result.StatusCode);
                Assert.IsType<Bike>(result.Value);
            }
        }
    }
    ```

- In GitHub Copilot Chat, click the ellipses `...` and select `Insert into New File` for the suggested `BikeControllerTest`.

- Copilot will add the code to a new empty file, but must be saved.

- Save the file by clicking pressing `Ctrl + S` or `Cmd + S`.

- Change directory to the `BikeShopAPI.Test/Controllers` folder`.

- Enter the file name `BikeControllerTest.cs` and click `Save`.

- Let's now verify that everything is operating as it should by running the unit tests in the terminal.

- Use the given command to launch the terminal and carry out the tests.

    ```sh
    cd src/BikeShopAPI.Test
    dotnet test
    ```

- The tests should run and pass.

    ```sh
    Starting test execution, please wait...
    A total of 1 test files matched the specified pattern.
    Passed!  - Failed:  0, Passed:  8, Skipped:  0, Total:  8
    ```
