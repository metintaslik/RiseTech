import 'package:flutter/material.dart';
import 'package:flutter_slidable/flutter_slidable.dart';
import 'package:risetechmobile/core/data.pipe.dart';
import 'package:risetechmobile/detail.dart';
import 'package:risetechmobile/model/contact.dart';
import 'package:risetechmobile/report.dart';

import 'model/directory.dart';

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Rise Technology',
      theme: ThemeData(
        primarySwatch: Colors.blue,
        visualDensity: VisualDensity.adaptivePlatformDensity,
      ),
      home: MyHomePage(title: 'Rise Technology Demo'),
    );
  }
}

class MyHomePage extends StatefulWidget {
  MyHomePage({Key key, this.title}) : super(key: key);
  final String title;

  @override
  _MyHomePageState createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  final _formKey = GlobalKey<FormState>();
  List<Directory> directories = new List<Directory>();

  TextEditingController nameController = new TextEditingController();
  TextEditingController surnameController = new TextEditingController();
  TextEditingController companyController = new TextEditingController();

  TextEditingController telephoneController = new TextEditingController();
  TextEditingController emailController = new TextEditingController();
  TextEditingController locationController = new TextEditingController();

  @override
  void initState() {
    super.initState();
    getDirectoriesData();
  }

  void getDirectoriesData() async {
    directories = await DataPipe.getDirectories();
    setState(() {});
  }

  void _createDirectory() {
    showDialog(
        context: context,
        builder: (BuildContext context) {
          return AlertDialog(
            content: Stack(
              overflow: Overflow.visible,
              children: <Widget>[
                Form(
                  key: _formKey,
                  child: Column(
                    mainAxisSize: MainAxisSize.min,
                    children: <Widget>[
                      Padding(
                        padding: EdgeInsets.all(8.0),
                        child: TextFormField(
                          controller: nameController,
                          decoration: InputDecoration(
                              labelText: "Name",
                              hintStyle: TextStyle(
                                  color: Colors.black,
                                  fontWeight: FontWeight.bold)),
                        ),
                      ),
                      Padding(
                        padding: EdgeInsets.all(8.0),
                        child: TextFormField(
                          controller: surnameController,
                          decoration: InputDecoration(
                              labelText: "Surname",
                              hintStyle: TextStyle(
                                  color: Colors.black,
                                  fontWeight: FontWeight.bold)),
                        ),
                      ),
                      Padding(
                        padding: EdgeInsets.all(8.0),
                        child: TextFormField(
                          controller: companyController,
                          decoration: InputDecoration(
                              labelText: "Company",
                              hintStyle: TextStyle(
                                  color: Colors.black,
                                  fontWeight: FontWeight.bold)),
                        ),
                      ),
                      Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: RaisedButton(
                          child: Text("Submit"),
                          onPressed: () async {
                            if (nameController.text == "" ||
                                surnameController.text == "" ||
                                companyController.text == "") {
                              await _showDialog("Error",
                                  "Fields required, cannot left be empty!");
                              return;
                            }
                            var result = await DataPipe.createDirectory(
                                new Directory(
                                    name: nameController.text,
                                    surname: surnameController.text,
                                    company: companyController.text));
                            if (result == null) return null;
                            setState(() {
                              directories.add(result);
                            });
                            setState(() {
                              nameController.text = "";
                              surnameController.text = "";
                              companyController.text = "";
                            });
                            Navigator.of(context).pop();
                            await _showDialog(
                                "Successful", "New directory recorded!");
                          },
                        ),
                      )
                    ],
                  ),
                ),
              ],
            ),
          );
        });
  }

  void _addContact(String uuid) {
    showDialog(
        context: context,
        builder: (BuildContext context) {
          return AlertDialog(
            content: Stack(
              overflow: Overflow.visible,
              children: <Widget>[
                Form(
                  key: _formKey,
                  child: Column(
                    mainAxisSize: MainAxisSize.min,
                    children: <Widget>[
                      Padding(
                        padding: EdgeInsets.all(8.0),
                        child: TextFormField(
                          controller: telephoneController,
                          decoration: InputDecoration(
                              labelText: "Telephone",
                              hintStyle: TextStyle(
                                  color: Colors.black,
                                  fontWeight: FontWeight.bold)),
                        ),
                      ),
                      Padding(
                        padding: EdgeInsets.all(8.0),
                        child: TextFormField(
                          controller: emailController,
                          decoration: InputDecoration(
                              labelText: "E-Mail",
                              hintStyle: TextStyle(
                                  color: Colors.black,
                                  fontWeight: FontWeight.bold)),
                        ),
                      ),
                      Padding(
                        padding: EdgeInsets.all(8.0),
                        child: TextFormField(
                          controller: locationController,
                          decoration: InputDecoration(
                              labelText: "Location",
                              hintStyle: TextStyle(
                                  color: Colors.black,
                                  fontWeight: FontWeight.bold)),
                        ),
                      ),
                      Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: RaisedButton(
                          child: Text("Submit"),
                          onPressed: () async {
                            if (telephoneController.text == "" ||
                                emailController.text == "" ||
                                locationController.text == "") {
                              await _showDialog("Error",
                                  "Fields required, cannot left be empty!");
                              return;
                            }
                            var result = await DataPipe.addContact(new Contact(
                                personId: uuid,
                                telephone: telephoneController.text,
                                email: emailController.text,
                                location: locationController.text));
                            if (result == null) return null;
                            setState(() {
                              telephoneController.text = "";
                              emailController.text = "";
                              locationController.text = "";
                            });
                            Navigator.of(context).pop();
                            await _showDialog(
                                "Successful", "Contact is added!");
                          },
                        ),
                      )
                    ],
                  ),
                ),
              ],
            ),
          );
        });
  }

  void _showDialog(String title, String text) async {
    await showDialog(
        context: context,
        builder: (BuildContext context) {
          return AlertDialog(
            title: new Text(title),
            content: new Text(text),
            actions: <Widget>[
              new RaisedButton(
                child: new Text(
                  'Kapat',
                ),
                onPressed: () {
                  Navigator.of(context).pop();
                },
              ),
            ],
          );
        });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(widget.title),
        actions: <Widget>[
          IconButton(
            icon: Icon(Icons.list_alt_outlined),
            onPressed: () {
              Navigator.of(context).push(MaterialPageRoute(
                  builder: (builder) => new ReportPage(
                        title: "Reports",
                      )));
            },
          ),
        ],
      ),
      body: SafeArea(
        child: ListView.builder(
          itemCount: directories.length,
          itemBuilder: (context, index) {
            var item = directories[index];
            return Slidable(
              actionPane: SlidableDrawerActionPane(),
              actionExtentRatio: 0.25,
              secondaryActions: <Widget>[
                IconSlideAction(
                  icon: Icons.contact_phone_sharp,
                  caption: "Contact",
                  onTap: () async {
                    await _addContact(item.uuid);
                  },
                ),
                IconSlideAction(
                  icon: Icons.visibility_off,
                  caption: "Hide",
                  onTap: () async {
                    var result = await DataPipe.inactiveDirectory(item.uuid);
                    if (result == null) return;
                    setState(() {
                      directories.remove(item);
                    });
                    _showDialog("Successful", "Directory hided!");
                  },
                ),
                IconSlideAction(
                  icon: Icons.person_remove,
                  caption: "Delete",
                  onTap: () async {
                    var result = await DataPipe.removeDirectory(item.uuid);
                    if (!result) return;
                    setState(() {
                      directories.remove(item);
                    });
                    _showDialog("Successful", "Directory removed!");
                  },
                ),
              ],
              child: Container(
                decoration: BoxDecoration(
                  border: Border.all(
                    width: 1,
                    color: Colors.black,
                  ),
                  borderRadius: const BorderRadius.all(
                    const Radius.circular(1),
                  ),
                ),
                child: ListTile(
                  contentPadding: EdgeInsets.fromLTRB(20, 0, 0, 0),
                  leading: CircleAvatar(
                    child: IconButton(
                      icon: Icon(Icons.person_rounded),
                      onPressed: () async {
                        Directory directory =
                            await DataPipe.getDirectory(item.uuid);
                        Navigator.of(context).push(new MaterialPageRoute(
                            builder: (context) => new DetailPage(
                                title:
                                    "${directory.name.toUpperCase()} ${directory.surname.toUpperCase()}",
                                directory: directory)));
                      },
                    ),
                  ),
                  title: Text(
                    "${item.name.toUpperCase()} ${item.surname.toUpperCase()}",
                    style: TextStyle(fontWeight: FontWeight.bold),
                  ),
                  subtitle: Text(
                    "${item.company}",
                    style: TextStyle(color: Colors.black87),
                  ),
                ),
              ),
            );
          },
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: _createDirectory,
        tooltip: 'Create Directory',
        child: Icon(Icons.add),
      ),
    );
  }
}
