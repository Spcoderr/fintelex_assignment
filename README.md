# 🚀 Fintelex Assignment

## 🧾 Overview
This project is developed using **.NET 8**. It provides a set of **RESTful APIs** along with **unit tests** to validate core functionalities. A Postman collection is included for easy API testing.

---

## ✅ Prerequisites

Ensure you have the following tools installed before setting up the project:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) with the workloads:
  - ASP.NET and Web Development
  - .NET Core Cross-Platform Development
- [Postman](https://www.postman.com/downloads/) for API testing

---

## ⚙️ Setting Up the Application

1. **Clone the Repository**

   ```bash
   git clone https://your-repository-url.git
   cd your-repository-folder
   ```

2. **Restore Dependencies**

   Open the solution in Visual Studio 2022 and restore NuGet packages, or run:

   ```bash
   dotnet restore
   ```

3. **Build the Project**

   ```bash
   dotnet build
   ```

4. **Run the Application**

   ```bash
   dotnet run
   ```

   Alternatively, press `F5` in Visual Studio to run in debug mode.

5. **Access the Application**

   Navigate to: [http://localhost:7691](http://localhost:7691)

---

## 🧪 Running Unit Tests

1. **Navigate to the Test Project**

   Use the terminal to move into the test project directory.

2. **Run Tests**

   ```bash
   dotnet test
   ```

3. **View Test Results**

   Test results will be displayed in the terminal. You can also use **Test Explorer** in Visual Studio for a more detailed view.

---

## 📬 Importing the Postman Collection

1. **Locate the Postman Collection**

   The file is located in the `Postman` folder:

   ```
   Postman/fintelex_assignment-postman-collection.json
   ```

2. **Import into Postman**

   - Open **Postman**
   - Click **Import**
   - Select the `.json` file and click **Open**

3. **Test the APIs**

   Ensure the application is running before sending requests.

---

## 📝 Additional Notes

- Modify the `appsettings.json` file for any configuration updates.
- Check application logs for error details during troubleshooting.

---

> 💡 For further support or questions, please contact the project maintainer.
