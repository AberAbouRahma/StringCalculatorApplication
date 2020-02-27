# StringCalculatorApplication
- The Application that takes input string with 0, 1 or 2 numbers, and will return their sum. 
- The appliction shall not process a single number when a delimiter appears within the input string
- The appliction shall not process more than 2 numbers, and the summing process shall throw IllegalOperationException when encountered
- The application assumes that there are two categories of delimiters: A) "'" and "\n" B) Custom Delimiters that appears between "//" and 
"\n" at the start of te input string
- The custom delimiter can consist of more than one character. It can contain digits but at least one NAN cahracter. 
- Numbers only cutom delimiters of any length is/are not allowed
- Numbers greater than 1000 Will count as a number parameter (although they are summed as zeros)
- (-VE) Numbers shall also count as a parameter and the summing process shall throw IllegalOperationException when encountered
