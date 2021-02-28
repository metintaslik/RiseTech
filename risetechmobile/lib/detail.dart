import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_slidable/flutter_slidable.dart';
import 'package:risetechmobile/core/data.pipe.dart';
import 'package:risetechmobile/model/directory.dart';

class DetailPage extends StatefulWidget {
  DetailPage({Key key, this.title, this.directory}) : super(key: key);
  final String title;
  final Directory directory;

  @override
  _DetailPageState createState() => _DetailPageState(this.directory);
}

class _DetailPageState extends State<DetailPage> {
  final Directory directory;
  _DetailPageState(this.directory);

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
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(widget.title),
      ),
      body: SafeArea(
        key: widget.key,
        child: ListView.builder(
          itemCount: directory.contacts.length,
          itemBuilder: (context, index) {
            var contact = directory.contacts[index];
            return Slidable(
              actionPane: SlidableDrawerActionPane(),
              actionExtentRatio: 0.25,
              secondaryActions: <Widget>[
                IconSlideAction(
                  icon: Icons.visibility_off,
                  caption: "Hide",
                  onTap: () async {
                    var result = await DataPipe.inactiveContact(contact.id);
                    if (result == null) return;
                    setState(() {
                      directory.contacts.remove(contact);
                    });
                    _showDialog("Successful", "Record is hided!");
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
                  title: Text("${contact.telephone} - ${contact.email}"),
                  subtitle: Text(
                    "${contact.location}",
                    style: TextStyle(fontStyle: FontStyle.italic),
                  ),
                ),
              ),
            );
          },
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () {},
        tooltip: 'Add Contact',
        child: Icon(Icons.quick_contacts_dialer),
      ),
    );
  }
}
