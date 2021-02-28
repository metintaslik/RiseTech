import 'dart:convert' as converter;
import 'package:http/http.dart' as http;
import 'package:risetechmobile/model/contact.dart';
import 'package:risetechmobile/model/directory.dart';
import 'package:risetechmobile/model/report.dart';

class DataPipe {
  static const String originUrl = "http://192.168.0.16:44380/api/";

  static Future<List<Directory>> getDirectories() async {
    final directories = new List<Directory>();
    try {
      final response = await http.get(originUrl + "Directories");
      final items = converter.jsonDecode(response.body);
      if (items.length != 0) {
        for (var i = 0; i < items.length; i++) {
          directories.add(Directory.fromJson(items[i]));
        }
      }
    } catch (e) {
      print(e);
    }
    return directories;
  }

  static Future<Directory> getDirectory(String uuid) async {
    try {
      Directory directory;
      Map<String, String> map = new Map<String, String>();
      map["Content-Type"] = "application/json";
      final request = http.Request('GET', Uri.parse(originUrl + "Directory"));
      request.body = '{"uuid": "$uuid"}';
      request.headers.addAll(map);
      final response = await request.send();
      final responseBody =
          converter.json.decode(await response.stream.bytesToString());
      directory = Directory.fromJson(responseBody);
      responseBody["Contacts"].forEach((c) {
        directory.contacts.add(Contact.fromJson(c));
      });
      return directory;
    } catch (e) {
      print(e);
      return null;
    }
  }

  static Future<Directory> createDirectory(Directory directory) async {
    try {
      Map<String, String> h = new Map<String, String>();
      h["Content-Type"] = "application/json";
      var response = await http.post(originUrl + "CreateDirectory",
          body:
              '{"name": "${directory.name}", "surname": "${directory.surname}", "company": "${directory.company}"}',
          headers: h);
      final items = converter.json.decode(response.body);
      var result = Directory.fromJson(items);
      return result;
    } catch (e) {
      print(e);
      return null;
    }
  }

  static Future<Directory> inactiveDirectory(String uuid) async {
    try {
      Directory directory;
      Map<String, String> map = new Map<String, String>();
      map["Content-Type"] = "application/json";
      final request =
          http.Request('PATCH', Uri.parse(originUrl + "InactiveDirectory"));
      request.body = '{"uuid": "$uuid"}';
      request.headers.addAll(map);
      final response = await request.send();
      directory = Directory.fromJson(
          converter.json.decode(await response.stream.bytesToString()));
      return directory;
    } catch (e) {
      print(e);
      return null;
    }
  }

  static Future<bool> removeDirectory(String uuid) async {
    try {
      Map<String, String> map = new Map<String, String>();
      map["Content-Type"] = "application/json";
      final request =
          http.Request('POST', Uri.parse(originUrl + "DeleteDirectory"));
      request.body = '{"uuid": "$uuid"}';
      request.headers.addAll(map);
      final response = await request.send();
      if (response.statusCode == 500) return false;
      var result = await response.stream.bytesToString();
      return true;
    } catch (e) {
      print(e);
      return false;
    }
  }

  static Future<Contact> addContact(Contact contact) async {
    try {
      Contact resultContact;
      Map<String, String> map = new Map<String, String>();
      map["Content-Type"] = "application/json";
      final response = await http.post(originUrl + "AddContact",
          body:
              '{ "personId": "${contact.personId}", "telephone": "${contact.telephone}", "email" : "${contact.email}", "location": "${contact.location}" }',
          headers: map);
      resultContact = Contact.fromJson(converter.json.decode(response.body));
      return resultContact;
    } catch (e) {
      print(e);
      return null;
    }
  }

  static Future<Contact> inactiveContact(int id) async {
    try {
      Contact contact;
      Map<String, String> map = new Map<String, String>();
      map["Content-Type"] = "application/json";
      final request =
          http.Request('PATCH', Uri.parse(originUrl + "InactiveContact"));
      request.body = '{"id": $id}';
      request.headers.addAll(map);
      final response = await request.send();
      contact = Contact.fromJson(
          converter.json.decode(await response.stream.bytesToString()));
      return contact;
    } catch (e) {
      print(e);
      return null;
    }
  }

  static Future<Report> reporters() async {
    try {
      Report reports = new Report();
      final response = await http.get(originUrl + "Reporter");
      final items = converter.json.decode(response.body);
      items["HighToLowCountByLocation"].forEach((i) {
        reports.highToLowCountByLocation.add(ReportCounter.fromJson(i));
      });
      items["DirectoryCountByLocation"].forEach((i) {
        reports.directoryCountByLocation.add(ReportCounter.fromJson(i));
      });
      items["TelephoneCountByLocation"].forEach((i) {
        reports.telephoneCountByLocation.add(ReportCounter.fromJson(i));
      });
      return reports;
    } catch (e) {
      print(e);
      return null;
    }
  }
}
