# 🚀 ASP.NET Core Session State Management Demo

A modern, professional demonstration of session state management in ASP.NET Core 7.0 using Razor Pages. This project showcases best practices for managing user data across multiple HTTP requests using a custom SessionManager pattern.

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-7.0-blue)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.0-purple)
![License](https://img.shields.io/badge/license-MIT-green)

## 📋 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Technical Stack](#technical-stack)
- [Architecture](#architecture)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Session Management Implementation](#session-management-implementation)
- [UI/UX Design](#uiux-design)
- [Configuration Options](#configuration-options)
- [Learning Objectives](#learning-objectives)
- [Contributing](#contributing)
- [License](#license)

## 🎯 Overview

This demo application demonstrates how to implement session state management in ASP.NET Core using a multi-step form. Users enter their personal information across three pages, with data persisted using session state between requests.

**Key Demonstration Points:**
- Custom SessionManager pattern for type-safe session access
- JSON serialization/deserialization of complex objects
- Distributed memory cache for session storage
- Modern, responsive UI with Bootstrap 5
- Clean architecture with separation of concerns

## ✨ Features

### Technical Features
- ✅ **Custom SessionManager Pattern**: Type-safe wrapper around HttpContext.Session
- ✅ **Generic Session Variables**: Strongly-typed session variable access
- ✅ **JSON Serialization**: Automatic serialization of complex objects
- ✅ **Distributed Cache Support**: Configured for distributed memory cache (extensible to Redis/SQL Server)
- ✅ **Razor Pages Architecture**: Clean, page-focused web application model

### UI/UX Features
- 🎨 **Modern Design**: Professional tech-focused interface
- 📱 **Responsive Layout**: Mobile-first design with Bootstrap 5
- 🎭 **Smooth Animations**: CSS transitions and animations
- 🌈 **Gradient Accents**: Tech-inspired color schemes
- 📊 **Progress Indicators**: Clear multi-step form progression
- 💡 **Educational Tooltips**: Technical notes explaining concepts

## 🛠 Technical Stack

| Technology | Version | Purpose |
|------------|---------|---------|
| ASP.NET Core | 7.0 | Web framework |
| C# | 11.0 | Programming language |
| Razor Pages | Latest | Page-focused web UI |
| Bootstrap | 5.x | CSS framework |
| System.Text.Json | Built-in | JSON serialization |
| Microsoft.Extensions.Caching | 7.0 | Session state caching |

## 🏗 Architecture

### Session Management Pattern

```
┌─────────────────────────────────────────┐
│         Razor Page (UI Layer)           │
│  - Index.cshtml / MoreDetail.cshtml     │
└──────────────┬──────────────────────────┘
               │
               ↓
┌─────────────────────────────────────────┐
│      Page Model (Logic Layer)           │
│  - IndexModel / MoreDetailModel         │
└──────────────┬──────────────────────────┘
               │
               ↓
┌─────────────────────────────────────────┐
│    SessionVar (Typed Access Layer)      │
│  - FirstName property                   │
│  - UserDataValues property              │
└──────────────┬──────────────────────────┘
               │
               ↓
┌─────────────────────────────────────────┐
│  SessionManager (Base Layer)            │
│  - Set<T>(key, value)                   │
│  - Get<T>(key)                          │
└──────────────┬──────────────────────────┘
               │
               ↓
┌─────────────────────────────────────────┐
│    HttpContext.Session (Framework)      │
│  - Distributed Memory Cache             │
└─────────────────────────────────────────┘
```

## 🚀 Getting Started

### Prerequisites

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0) or later
- Visual Studio 2022, VS Code, or JetBrains Rider
- Basic knowledge of C# and ASP.NET Core

### Installation & Running

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd M02/Demo
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the project**
   ```bash
   dotnet build
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Open in browser**
   ```
   Navigate to: https://localhost:5001
   or: http://localhost:5000
   ```

### Quick Test

1. Enter a first name on the home page
2. Click "Next" to proceed to the details page
3. Fill in all required fields (Last Name, Address, City, State, ZIP)
4. Click "Preview" to review your information
5. Click "Save It!" to complete the demo

## 📁 Project Structure

```
Demo/
├── Models/
│   └── UserData.cs                 # Data model for user information
├── Sessions/
│   ├── SessionManager.cs           # Base session management class
│   └── SessionVar.cs               # Typed session variable access
├── Pages/
│   ├── Index.cshtml                # Step 1: Name entry
│   ├── Index.cshtml.cs
│   ├── MoreDetail.cshtml           # Step 2: Detailed information
│   ├── MoreDetail.cshtml.cs
│   ├── Confirm.cshtml              # Step 3: Review and confirm
│   ├── Confirm.cshtml.cs
│   ├── Privacy.cshtml              # Privacy policy page
│   ├── Privacy.cshtml.cs
│   ├── Error.cshtml                # Error handling page
│   ├── Error.cshtml.cs
│   └── Shared/
│       ├── _Layout.cshtml          # Main layout template
│       └── _ValidationScriptsPartial.cshtml
├── wwwroot/
│   ├── css/
│   │   └── site.css                # Custom styles
│   ├── js/
│   │   └── site.js                 # Custom JavaScript
│   ├── lib/                        # Third-party libraries
│   └── *.png                       # Images
├── Program.cs                      # Application entry point
├── appsettings.json                # Configuration
└── Demo.csproj                     # Project file
```

## 🔧 Session Management Implementation

### 1. SessionManager (Base Class)

The `SessionManager` class provides generic methods for storing and retrieving any type of object from session state:

```csharp
public class SessionManager
{
    public static HttpContext HttpContext => new HttpContextAccessor().HttpContext;

    public static void Set(string key, object value)
    {
        HttpContext.Session.SetString(key, JsonSerializer.Serialize(value));
    }

    public static T? Get<T>(string key)
    {
        var value = HttpContext.Session.GetString(key);
        return value == null ? default : JsonSerializer.Deserialize<T>(value);
    }
}
```

**Key Features:**
- Generic type support for any serializable object
- Automatic JSON serialization/deserialization
- Static access for convenience

### 2. SessionVar (Typed Access)

The `SessionVar` class extends `SessionManager` to provide strongly-typed properties:

```csharp
public class SessionVar : SessionManager
{
    public static string FirstName 
    { 
        get => Get<string>("FirstName");
        set => Set("FirstName", value); 
    }

    public static UserData UserDataValues
    {
        get => Get<UserData>("UserDataValues");
        set => Set("UserDataValues", value);
    }
}
```

**Benefits:**
- Type-safe access to session data
- IntelliSense support
- Centralized session key management
- Easy to extend with new session variables

### 3. Configuration (Program.cs)

```csharp
// Configure distributed memory cache
builder.Services.AddDistributedMemoryCache();

// Enable session state
builder.Services.AddSession();

// Required for SessionManager to access HttpContext
builder.Services.AddHttpContextAccessor();

// Add session middleware to pipeline
app.UseSession();
```

## 🎨 UI/UX Design

### Design Philosophy

The UI follows modern web design principles with a tech-focused aesthetic:

- **Color Palette**: Tech-inspired gradients (cyan to purple)
- **Typography**: Inter font family for clean, professional look
- **Spacing**: Generous whitespace for readability
- **Animations**: Smooth transitions and hover effects
- **Responsiveness**: Mobile-first approach

### Key Design Elements

1. **Gradient Accents**: `linear-gradient(135deg, #00d4ff, #8b5cf6)`
2. **Card Shadows**: Elevated card design with depth
3. **Form Controls**: Modern rounded inputs with focus effects
4. **Buttons**: Gradient backgrounds with hover animations
5. **Data Display**: Styled containers with left border accents

### Custom CSS Variables

```css
--primary-color: #0d6efd;
--tech-accent: #00d4ff;
--tech-purple: #8b5cf6;
--dark-bg: #1a1d29;
--shadow-lg: 0 10px 25px rgba(0,0,0,0.15);
```

## ⚙️ Configuration Options

### Distributed Memory Cache (Default)

```csharp
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
```

**Pros:**
- Simple setup, no external dependencies
- Fast in-process storage
- Perfect for development and single-server deployments

**Cons:**
- Not suitable for load-balanced environments
- Data lost on application restart

### SQL Server Cache (Alternative)

```csharp
// Install tool: dotnet tool install --global dotnet-sql-cache
// Create table: dotnet sql-cache create "ConnectionString" dbo SessionState

builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.SchemaName = "dbo";
    options.TableName = "SessionState";
});
```

**Pros:**
- Persistent across application restarts
- Works with load-balanced servers
- Familiar SQL Server infrastructure

**Cons:**
- Requires SQL Server setup
- Slightly slower than memory cache

### Redis Cache (Alternative)

```csharp
// NuGet: Microsoft.Extensions.Caching.StackExchangeRedis

builder.Services.AddStackExchangeRedisCache(options => 
{
    options.Configuration = builder.Configuration.GetConnectionString("AzureRedis");
});
```

**Pros:**
- High performance
- Scales horizontally
- Cloud-ready (Azure Redis Cache)

**Cons:**
- Requires Redis server
- Additional infrastructure cost

## 📚 Learning Objectives

This demo helps you understand:

1. ✅ **Session State Basics**
   - What is session state and why use it
   - How sessions work in ASP.NET Core
   - Session lifecycle and expiration

2. ✅ **Implementation Patterns**
   - Creating custom session managers
   - Type-safe session access
   - Generic programming with sessions

3. ✅ **Serialization**
   - JSON serialization with System.Text.Json
   - Serializing complex objects
   - Handling null values

4. ✅ **Architecture**
   - Separation of concerns
   - Reusable session management patterns
   - Clean code principles

5. ✅ **Modern Web Development**
   - Razor Pages architecture
   - Bootstrap 5 integration
   - Responsive design
   - Progressive enhancement

## 🧪 Testing the Application

### Manual Testing Checklist

- [ ] Navigate to home page and enter a name
- [ ] Verify name persists to the details page
- [ ] Fill out all form fields with valid data
- [ ] Verify data displays correctly on confirmation page
- [ ] Test form validation (try submitting empty fields)
- [ ] Test navigation (back button, start over)
- [ ] Test on mobile device or responsive view
- [ ] Clear cookies and test session expiration

### Expected Behavior

1. **Session Persistence**: Data should persist across page navigations
2. **Form Validation**: Required fields should be enforced
3. **Responsive Design**: UI should adapt to different screen sizes
4. **Animations**: Smooth transitions and hover effects
5. **Navigation**: All links and buttons should work correctly

## 🤝 Contributing

Contributions are welcome! This is an educational project, so improvements to:

- Documentation clarity
- Code comments
- UI/UX enhancements
- Additional session storage examples
- Testing examples

Please ensure any code follows the existing patterns and includes appropriate comments.

## 📄 License

This project is provided as-is for educational purposes. Feel free to use and modify as needed for learning.

## 🔗 Additional Resources

- [ASP.NET Core Session State Documentation](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state)
- [Razor Pages in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/razor-pages)
- [Bootstrap 5 Documentation](https://getbootstrap.com/docs/5.0)
- [System.Text.Json Documentation](https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-overview)

## 📧 Support

For questions or issues related to this demo:

1. Check the code comments for inline documentation
2. Review the ASP.NET Core documentation
3. Examine the Session Management Implementation section above

---

**Built with ❤️ for learning ASP.NET Core**

*Last Updated: January 2026*

