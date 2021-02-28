import 'package:flutter/material.dart';
import 'package:risetechmobile/core/data.pipe.dart';
import 'package:risetechmobile/model/report.dart';

class ReportPage extends StatefulWidget {
  ReportPage({Key key, this.title}) : super(key: key);
  final String title;

  @override
  _ReportPageState createState() => _ReportPageState();
}

class _ReportPageState extends State<ReportPage> {
  Report reports;

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    getReportData();
  }

  void getReportData() async {
    reports = await DataPipe.reporters();
    setState(() {});
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(widget.title),
      ),
      body: SafeArea(
          key: widget.key,
          child: ListView(
            children: [
              Card(
                child: Column(
                  children: [
                    Center(
                        child: Text(
                      "High To Low Count By Location",
                      style: TextStyle(fontSize: 25),
                    )),
                    Divider(
                      color: Colors.black,
                      thickness: 1,
                      height: 1,
                    ),
                    ListView.builder(
                      itemCount: reports.highToLowCountByLocation.length,
                      itemBuilder: (builder, index) {
                        var item = reports.highToLowCountByLocation[index];
                        return ListTile(
                          title: Text("${item.location} - ${item.counter}"),
                        );
                      },
                    ),
                  ],
                ),
              )
            ],
          )),
    );
  }
}
