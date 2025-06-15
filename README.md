
# PDF Form Filler Web Application

A full-stack web application that automatically fills PDF forms with user input. Built with C#/.NET backend and React frontend.

## 🎯 What This App Does

1. **User fills a web form** with their information
2. **Backend receives the data** and overlays it onto a blank PDF template
3. **User downloads the completed PDF** automatically

## 🏗️ Architecture Overview

```
Frontend (React)          Backend (C#/.NET)         PDF Processing
┌─────────────────┐       ┌──────────────────┐      ┌─────────────────┐
│   PdfForm.jsx   │────→  │  PdfController   │────→ │  PdfSharp       │
│   (User Input)  │       │  (API Endpoint)  │      │  (Fill PDF)     │
└─────────────────┘       └──────────────────┘      └─────────────────┘
         │                          │                         │
         │                          │                         │
         ▼                          ▼                         ▼
┌─────────────────┐       ┌──────────────────┐      ┌─────────────────┐
│   PdfForm.css   │       │   FormData.cs    │      │   Filled PDF    │
│   (Styling)     │       │   (Data Model)   │      │   (Download)    │
└─────────────────┘       └──────────────────┘      └─────────────────┘
```

## 📁 Project Structure

### Backend (ASP.NET Core Web API)
```
MyPdfFillerApi/
├── Controllers/
│   └── PdfController.cs          # API endpoint for PDF generation
├── Models/
│   └── FormData.cs               # Data model for form fields
├── PDFs/
│   └── Requerimento de Intenção de Venda em branco.pdf  # Template
├── Program.cs                    # App configuration
└── MyPdfFillerApi.csproj        # Project file
```

### Frontend (React)
```
pdf-filler-frontend/
├── src/
│   ├── components/
│   │   ├── PdfForm.jsx          # Main form component
│   │   └── PdfForm.css          # Form styling
│   ├── App.js                   # Main app component
│   └── index.js                 # Entry point
├── public/
└── package.json                 # Dependencies
```

## 🚀 Setup Instructions

### Prerequisites
- [.NET 6 SDK or later](https://dotnet.microsoft.com/download)
- [Node.js 16+ and npm](https://nodejs.org/)
- [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio](https://visualstudio.microsoft.com/)

### Backend Setup (C#/.NET)

1. **Create the ASP.NET Core Web API project:**
   ```bash
   dotnet new webapi -n MyPdfFillerApi
   cd MyPdfFillerApi
   ```

2. **Install required NuGet packages:**
   ```bash
   dotnet add package PdfSharp
   dotnet add package Microsoft.AspNetCore.Cors
   ```

3. **Create the folder structure:**
   ```bash
   mkdir Controllers Models PDFs
   ```

4. **Add the generated files:**
   - Copy `PdfController.cs` to `Controllers/` folder
   - Copy `FormData.cs` to `Models/` folder
   - Copy your blank PDF to `PDFs/` folder

5. **Configure CORS in Program.cs:**
   ```csharp
   var builder = WebApplication.CreateBuilder(args);

   builder.Services.AddControllers();
   builder.Services.AddCors(options =>
   {
       options.AddPolicy("AllowReactApp",
           builder => builder
               .WithOrigins("http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader());
   });

   var app = builder.Build();

   app.UseCors("AllowReactApp");
   app.UseAuthorization();
   app.MapControllers();

   app.Run();
   ```

6. **Run the backend:**
   ```bash
   dotnet run
   ```
   Backend will run on `https://localhost:5001` or `http://localhost:5000`

### Frontend Setup (React)

1. **Create the React app:**
   ```bash
   npx create-react-app pdf-filler-frontend
   cd pdf-filler-frontend
   ```

2. **Create the components folder:**
   ```bash
   mkdir src/components
   ```

3. **Add the generated files:**
   - Copy `PdfForm.jsx` to `src/components/`
   - Copy `PdfForm.css` to `src/components/`

4. **Update src/App.js:**
   ```jsx
   import React from 'react';
   import PdfForm from './components/PdfForm';
   import './App.css';

   function App() {
     return (
       <div className="App">
         <PdfForm />
       </div>
     );
   }

   export default App;
   ```

5. **Update the API URL in PdfForm.jsx:**
   ```jsx
   // Change this line in handleSubmit function:
   const response = await fetch('http://localhost:5000/api/pdf/fill', {
   ```

6. **Run the frontend:**
   ```bash
   npm start
   ```
   Frontend will run on `http://localhost:3000`

## 🔧 How It Works (Step by Step)

### 1. User Interaction
- User opens the web app in their browser
- Fills out the form with their information
- Clicks "Gerar PDF" button

### 2. Frontend Processing
- React collects all form data into a JavaScript object
- Sends HTTP POST request to backend API
- Shows loading state while waiting for response

### 3. Backend Processing
- ASP.NET Core receives the JSON data
- Deserializes it into a `FormData` C# object
- Loads the blank PDF template using PdfSharp
- Overlays the user's text at predetermined coordinates
- Converts the filled PDF to a byte array

### 4. Response & Download
- Backend returns the PDF as a file response
- Frontend receives the PDF blob
- Creates a temporary download link
- Automatically triggers the download
- User gets the filled PDF file

## 🎨 Customization Guide

### Adjusting Text Positions
The coordinates in `PdfController.cs` are estimates. To fine-tune them:

1. **Test with sample data**
2. **Check the output PDF**
3. **Adjust coordinates:**
   - Increase X to move text right
   - Decrease X to move text left
   - Increase Y to move text up
   - Decrease Y to move text down

### Adding New Fields
1. **Add property to FormData.cs:**
   ```csharp
   public string NewField { get; set; } = "";
   ```

2. **Add input to PdfForm.jsx:**
   ```jsx
   <div className="form-group">
     <label htmlFor="newField">New Field:</label>
     <input
       type="text"
       id="newField"
       name="newField"
       value={formData.newField}
       onChange={handleChange}
       required
     />
   </div>
   ```

3. **Add text overlay in PdfController.cs:**
   ```csharp
   gfx.DrawString(data.NewField, font, brush, new XPoint(x, y));
   ```

## 🐛 Troubleshooting

### Common Issues

1. **CORS Error:**
   - Make sure CORS is configured in Program.cs
   - Check that frontend URL matches the CORS policy

2. **PDF Not Found:**
   - Verify the PDF file path in PdfController.cs
   - Make sure the blank PDF is in the correct folder

3. **Text Not Appearing:**
   - Check the coordinates (might be outside page bounds)
   - Verify font name and size
   - Ensure the text color isn't white on white background

4. **Download Not Working:**
   - Check browser's download settings
   - Verify the Content-Type header is set correctly

### Testing the API
You can test the backend independently using tools like Postman:

**POST** `http://localhost:5000/api/pdf/fill`
**Body (JSON):**
```json
{
  "companyName": "Test Company",
  "companyCnpj": "12.345.678/0001-90",
  "personName": "John Doe",
  // ... other fields
}
```

## 📱 Mobile Responsiveness

The app is fully responsive and works on:
- ✅ Desktop computers
- ✅ Tablets
- ✅ Mobile phones
- ✅ Different screen orientations

## 🔒 Security Considerations

For production deployment, consider:
- Input validation and sanitization
- Rate limiting
- File upload size limits
- HTTPS enforcement
- Authentication if needed

## 🚀 Deployment Options

### Backend
- Azure App Service
- AWS Elastic Beanstalk
- Docker containers
- IIS on Windows Server

### Frontend
- Vercel
- Netlify
- Azure Static Web Apps
- AWS S3 + CloudFront

## 📚 Learning Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [React Documentation](https://reactjs.org/docs/)
- [PdfSharp Documentation](https://pdfsharp.net/wiki/)
- [CSS Grid and Flexbox Guide](https://css-tricks.com/snippets/css/complete-guide-grid/)

## 🎉 Next Steps

1. **Test the basic functionality**
2. **Fine-tune the PDF coordinates**
3. **Add more validation**
4. **Implement error logging**
5. **Add unit tests**
6. **Deploy to production**

---

**Happy coding! 🚀**
