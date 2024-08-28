## 1.Model Binding
    - Model binding is a feature of asp.net core that reads values from http requests and pass them as arguments to the action method.

```Text
HTTP Request => Routing => Model Binding (Form Fields, Request body, Route Data, Query String parameters) => Controler
```

## 2. Query String vs Route Data

### Query String
```C#
    //?param1=value1&param2=value2
    [Route("store/book)]
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