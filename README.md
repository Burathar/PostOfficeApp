# PostOfficeApp

An app where a 'post office manager' can register that some has received a mail or package. A web front-end will show all to-be collected mail. The 'post office manager' can then change the mail's status to collected.
The app uses a textfile to store the applicationdata. A file named "PostOfficeGrid.txt" will be generated in the hostfolder.
By changing the ICellRepository implementation from LocalFileCellRepository to MySqlCellRepository in [HomeController.cs](https://github.com/Burathar/PostOfficeApp/blob/master/ASPPostOffice/ASPPostOffice/Controllers/HomeController.cs), a MySql database can be used instead of the textfile.

## Authors

* **Burathar** - *Initial work* - [Burathar](https://github.com/Burathar)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

## Documentation
* [Analysis](https://github.com/Burathar/PostOfficeApp/blob/master/Analysis.md)
