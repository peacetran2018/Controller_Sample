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