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
                    ExpansionTile(
                      title: Text(
                        "High To Low Count By Location",
                        style: TextStyle(
                            fontSize: 18, fontWeight: FontWeight.bold),
                      ),
                      children: [
                        ListView.builder(
                          shrinkWrap: true,
                          itemCount: reports.highToLowCountByLocation.length,
                          itemBuilder: (builder, index) {
                            var item = reports.highToLowCountByLocation[index];
                            return Container(
                              margin: const EdgeInsets.all(1.0),
                              decoration: BoxDecoration(
                                border: Border.all(
                                    color: Colors.blueAccent, width: 2),
                              ),
                              child: ListTile(
                                title:
                                    Text("${item.location} - ${item.counter}"),
                              ),
                            );
                          },
                        ),
                      ],
                    ),
                    Container(
                      margin: const EdgeInsets.all(3.0),
                      child: Divider(
                        color: Colors.blueGrey,
                        thickness: 3,
                        height: 1,
                      ),
                    ),
                    ExpansionTile(
                      title: Text(
                        "Directory Count By Location",
                        style: TextStyle(
                            fontSize: 18, fontWeight: FontWeight.bold),
                      ),
                      children: [
                        ListView.builder(
                          shrinkWrap: true,
                          itemCount: reports.directoryCountByLocation.length,
                          itemBuilder: (builder, index) {
                            var item = reports.directoryCountByLocation[index];
                            return Container(
                              margin: const EdgeInsets.all(1.0),
                              decoration: BoxDecoration(
                                border: Border.all(
                                    color: Colors.blueAccent, width: 2),
                              ),
                              child: ListTile(
                                title:
                                    Text("${item.location} - ${item.counter}"),
                              ),
                            );
                          },
                        ),
                      ],
                    ),
                    Container(
                      margin: const EdgeInsets.all(3.0),
                      child: Divider(
                        color: Colors.blueGrey,
                        thickness: 3,
                        height: 1,
                      ),
                    ),
                    ExpansionTile(
                      title: Text(
                        "Telephone Number Count By Location",
                        style: TextStyle(
                            fontSize: 18, fontWeight: FontWeight.bold),
                      ),
                      children: [
                        ListView.builder(
                          shrinkWrap: true,
                          itemCount: reports.telephoneCountByLocation.length,
                          itemBuilder: (builder, index) {
                            var item = reports.telephoneCountByLocation[index];
                            return Container(
                              margin: const EdgeInsets.all(1.0),
                              decoration: BoxDecoration(
                                border: Border.all(
                                    color: Colors.blueAccent, width: 2),
                              ),
                              child: ListTile(
                                title:
                                    Text("${item.location} - ${item.counter}"),
                              ),
                            );
                          },
                        ),
                      ],
                    ),
                  ],
                ),
              )
            ],
          )),
    );
  }
}
