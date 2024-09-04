## 1.Model Binding
- Model binding is a feature of asp.net core that reads values from http requests and pass them as arguments to the action method.

```Text
HTTP Request => Routing => Model Binding (Form Fields, Request body, Route Data, Query String parameters) => Controler
```

## 2. Query String vs Route Data

### Query String
```C#
    //?param1=value1&param2=value2
    [Route("store/book")]
    public IActionResult Index(int? bookid){     
        if(bookid.HasValue == false){
            return Content("Book ID is not supplied or blank");
        }

        return RedirectToAction("Store", "Book", new { id = bookid });
    }
```

### Route Data
```C#
    // /value1/value2
    // /{param1}/{param2}
    [Route("store/book/{bookid?}")]
    public IActionResult Index(int? bookid){
        if(bookid.HasValue == false){
            return Content("Book ID is not supplied or blank");
        }

        return RedirectToAction("Store", "Book", new { id = bookid });
    }
```
## 3. FromQuery VS FromRoute
    - FromQuery and FromRoute are a specific priority to get the value from QueryString First or from RouteData first.
  
### [FromQuery]
```C#
    //Gets the value from query string only
    public IActionResult Index([FromQuery] type parameter){

    }
```

### [FromRoute]
```C#
    //Gets the value from route data only
    public IActionResult Index([FromRoute] type parameter){

    }
```

## 4. Model Class
    - is a class represents structure of data (as properties) that you would like to receive from the request and/or send to the response.
  
### Sample
```C#
    //Model Class
    public class BookModel{
        public int bookid {get;set;}
        public string author {get;set;}
    }

    //Controller
    public IActionResult Index(BookModel book){//priority from route data
        return Content($"Book id { book.id }");
    }
```
    - To specific from route data by class we do same as lession 3
### [FromQuery]
```C#
    //Model Class
    public class BookModel{
        public int bookid {get;set;}
        public string author {get;set;}
    }

    //Controller
    public IActionResult Index([FromQuery] BookModel book){//specific class get value from query string
        return Content($"Book id { book.id }");
    }
```

### [FromRoute]
```C#
    //Model Class
    public class BookModel{
        public int bookid {get;set;}
        public string author {get;set;}
    }

    //Controller
    public IActionResult Index([FromRoute] BookModel book){//specific class get value from route data
        return Content($"Book id { book.id }");
    }
```
    - We also can specific FromQuery or FromRoute for each properties. if we use specific from query or from route then the parameter from controller no need add specfic [FromQuery] or [FromRoute]

### [FromQuery] [FromRoute] for properties
```C#
    //Model Class
    public class BookModel{
        [FromQuery]
        public int bookid {get;set;}
        [FromRoute]
        public string author {get;set;}
    }

    //Controller
    public IActionResult Index(BookModel book){//no need specific to get value from where
        return Content($"Book id { book.id }");
    }
```

## 5. Form-urlencoded VS Form-data
    - Basically, those request body are same but form-data can attach the file

### Syntax
```JQuery
    Content-type: application/x-www-form-urlencoded

    Content-type: multipart/form-data
```

## 6. Model State
    - There are 3 types:
      - IsValid: Specifics whether there is at least one validation error or not (true or false).
      - Values: Contains each model property value with corresponding "Errors" property that contains list of validation errors of that model property.
      - ErrorCount: Returns number of errors.

### Sample
```C#
    //Person model class
    public class Person
    {
        //Validate PersonName cannot but NULL or BLANK
        //To custom error message Required(ErrorMessage = "Message")
        [Required(ErrorMessage = "Person Name cannot be NULL or BLANK")]
        public string? PersonName { get; set; }
        public string? Email {get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public double? Price { get; set; }

        public override string ToString()
        {
            return $"Person object - Person Name: {PersonName}, Email: {Email}, Phone: {Phone}, Password: {Password}, Confirm Password: {ConfirmPassword}, Price: {Price}";
        }
    }

    //HomeController
    public IActionResult Index(Person person)
        {
            if(!ModelState.IsValid){
                // Using foreach to loop to get ModelState Values and Errors String
                // List<string> errors = new List<string>();
                // foreach(var value in ModelState.Values){
                //     foreach(var error in value.Errors){
                //         errors.Add(error.ErrorMessage);
                //     }
                // }
                // var errorString = string.Join("\n", errors);
                // return BadRequest(errorString);
                
                // Using LINQ to get ModelState Values and Errors String
                List<string> errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                var errorString = string.Join("\n", errors);
                return BadRequest(errorString);
            }
            return Content($"{person.ToString()}");
        }
```

## 7. Model Validation
    - There are few types of validation
      - Required
      - StringLength
      - Range
      - RegularExpression
      - EmailAddress
      - Phone
      - Compare
      - Url
      - ValidNever

### Sample
```C#
    public class Person
    {
        //Validate PersonName cannot but NULL or BLANK
        //To custom error message Required(ErrorMessage = "Message")
        [Required(ErrorMessage = "{0} cannot be NULL or BLANK")]
        [Display(Name = "Person Name")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "{0} cannot less than {2} or greater than {1}")]//{0}: Property name, {1}: Max value, {2}: Min value
        [RegularExpression("^[A-Za-z .]$", ErrorMessage = "{0} should contain only alphabets, space and dot.")]//Check alphalbet on person name
        public string? PersonName { get; set; }

        //Check email format
        [EmailAddress(ErrorMessage = "{0} should be a proper email address")]
        public string? Email {get; set; }

        //Check phone format
        [Phone()]
        //Maximum 20 digit
        //Minimum 1 digit
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Invalid {0} number")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "{0} cannot be blank")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "{0} cannot be blank")]
        //Compare(Property Name)
        //{0}: ConfirmPassword Property name
        //{1}: Password Property name
        [Compare("Password", ErrorMessage = "{0} and {1} are not match")]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }
        
        [Range(0, 999.99, ErrorMessage = "{0} cannot out of range between {1} to {2}")]
        public double? Price { get; set; }

        public override string ToString()
        {
            return $"Person object - Person Name: {PersonName}, Email: {Email}, Phone: {Phone}, Password: {Password}, Confirm Password: {ConfirmPassword}, Price: {Price}";
        }
    }
```

## 8. Custom Validation with multiple object

### Sample
```C#
    public class DateRangeValidatorAttribute : ValidationAttribute
    {
        public string OtherPropertyName { get; set; }
        public DateRangeValidatorAttribute(string otherPropertyName){
            OtherPropertyName = otherPropertyName;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value != null){
                //Get to_date
                DateTime to_date = Convert.ToDateTime(value);

                //Get preference from property from_date
                PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(OtherPropertyName);

                if(otherProperty != null){
                    DateTime from_date = Convert.ToDateTime(otherProperty.GetValue(validationContext.ObjectInstance));
                    if(from_date > to_date){
                        return new ValidationResult(ErrorMessage);
                    }
                    else{
                        return ValidationResult.Success;
                    }
                }
            }
            return null;
        }
    }
```

### Usage
```C#
    [DateRangeValidator("FromDate", ErrorMessage = "'From Date' should be older than or equal to 'To Date'")]
    [Display(Name = "To Date")]
    public DateTime? ToDate { get; set; }
```

## 9. IValidatableObject

### Syntax
```C#
    class ClassName: IValidatableObject{
        //model properties here
        public IEnumrable<ValidationResult> Validate(ValidationContext validationContext){
            if(condition){
                yield return new ValidationResult("Error Message");
            }
        }
    }
```

## 10. Bind VS BindNever
    - Bind is passing data to specific attribute of a model class
    - BindNever is no pass data to specific attribute of a model class
  
### Sample
```C#
    //Bind
    [Route("register")]
    public IActionResult Register([Bind(nameof(Person.PersonName), nameof(Person.Email), nameof(Person.Password), nameof(Person.ConfirmPassword))]Person person)
    {
        //...logic
    }

    //BindNever
    [BindNever]
    public DateTime? DateOfBirth { get; set; }
```

## 11. FromBody
    - To pass JSON data to model class in asp.net core has keyword [FromBody] same as [FromQuery] or [FromRoute]

### Sample
```JSON
    {
        "PersonName": "Peace",
        "Email": "Email@gmail.com"
    }
```
```C#
    public IActionResult Register([FromBody]Person person)
    {
        return Content($"Person Name: {person.PersonName} and Email: {person.Email}");
    }
```

### Output
```Text
    Person Name: Peace and Email: Email@gmail.com
```

## 12. Input Formatters

### Configure
```C#
    //program.cs
    //Enable Xml SerializerFormatter
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllers();
    //to controller understand XML format
    builder.Services.AddControllers().AddXmlSerializerFormatters();
    var app = builder.Build();

    app.UseStaticFiles();
    app.UseRouting();
    app.MapControllers();

    app.Run();
```
## 13. Custom Model Binder
    - In case, if our model don't have First Name and Last Name but has Full Name.
    - Which is parameters from API doesn't has First Name and Last Name. So we need custom model binder

### Sample
```C#
    public class PersonModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Person person = new Person();
            //Fist Name and Last Name
            if(bindingContext.ValueProvider.GetValue("FirstName").Length > 0){
                person.PersonName = bindingContext.ValueProvider.GetValue("FirstName").FirstValue;
                if(bindingContext.ValueProvider.GetValue("LastName").Length > 0){
                    person.PersonName += " " + bindingContext.ValueProvider.GetValue("LastName").FirstValue;
                }
            }
            //Email
            if(bindingContext.ValueProvider.GetValue("Email").Length > 0){  
                person.Email = bindingContext.ValueProvider.GetValue("Email").FirstValue;
            }

            //Phone
            if(bindingContext.ValueProvider.GetValue("Phone").Length > 0){  
                person.Phone = bindingContext.ValueProvider.GetValue("Phone").FirstValue;
            }

            //Password
            if(bindingContext.ValueProvider.GetValue("Password").Length > 0){  
                person.Password = bindingContext.ValueProvider.GetValue("Password").FirstValue;
            }

            //ConfirmPassword
            if(bindingContext.ValueProvider.GetValue("ConfirmPassword").Length > 0){  
                person.ConfirmPassword = bindingContext.ValueProvider.GetValue("ConfirmPassword").FirstValue;
            }

            //Price
            if(bindingContext.ValueProvider.GetValue("Price").Length > 0){  
                person.Price = Convert.ToDouble(bindingContext.ValueProvider.GetValue("Price").FirstValue);
            }

            //DOB
            if(bindingContext.ValueProvider.GetValue("DateOfBirth").Length > 0){  
                person.DateOfBirth = Convert.ToDateTime(bindingContext.ValueProvider.GetValue("DateOfBirth").FirstValue);
            }

            //Returns model object after reading data from the request
            bindingContext.Result = ModelBindingResult.Success(person);
            return Task.CompletedTask;
        }
    }
```

### Usage
```C#
    public IActionResult Register([FromBody] [ModelBinder(BinderType = typeof(PersonModelBinder))] Person person){
        //logic
    }
```

## 14. Custom Model binder provider

### Configure
```C#
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllers(options => {
        options.ModelBinderProviders.Insert(0, new PersonBinderProvider());//Add this binder provider to service controllers
        //index 0 is first place
    });
```

### Create Binder Provider
```C#
    public class PersonBinderProvider : IModelBinderProvider
    {
        //This Binder Provider will be called only 1 time when the first program running
        //Next time onwards will not call anymore.
        public IModelBinder? GetBinder(ModelBinderProviderContext context){
            if(context.Metadata.ModelType == typeof(Person)){
                return new BinderTypeModelBinder(typeof(PersonModelBinder));
            }
            return null;
        }
    }
```

## 15. Collection binding
    - Passing data to a list collection in model

### Sample
```C#
    //Person.cs
    //...
    public List<string?> Tags{ get; set; } = new List<string?>();
    //...
```

### Usage
```JSON
    //From request body
    {
        "PersonName":"Peace",
        "Email":"peacetran@gmail.com",
        "Phone": "123111",
        "Password": "1234",
        "ConfirmPassword": "1234",
        "DateOfBirth": "1992-06-18",
        "Price": 11.5,
        "Tags": ["#dotnet","#CSharp"]
    }
```