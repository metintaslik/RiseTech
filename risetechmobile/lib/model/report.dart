import 'package:flutter/cupertino.dart';

class Report {
  List<ReportCounter> highToLowCountByLocation = new List<ReportCounter>();
  List<ReportCounter> directoryCountByLocation = new List<ReportCounter>();
  List<ReportCounter> telephoneCountByLocation = new List<ReportCounter>();
}

class ReportCounter {
  String location;
  int counter;

  ReportCounter({this.location, this.counter});

  factory ReportCounter.fromJson(Map<String, dynamic> json) {
    return ReportCounter(location: json["Location"], counter: json["Counter"]);
  }

  static List<ReportCounter> fromJsonList(list) {
    if (list == null) return null;
    return list.map((item) => ReportCounter.fromJson(item)).toList();
  }
}
