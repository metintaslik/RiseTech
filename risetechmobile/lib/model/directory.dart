import 'package:risetechmobile/model/contact.dart';

class Directory {
  String uuid;
  String name;
  String surname;
  String company;
  bool isActive;
  List<Contact> contacts = new List<Contact>();

  Directory({
    this.uuid,
    this.name,
    this.surname,
    this.company,
    this.isActive,
  });

  factory Directory.fromJson(Map<String, dynamic> json) {
    return Directory(
      uuid: json["Uuid"],
      name: json["Name"],
      surname: json["Surname"],
      company: json["Company"],
      isActive: json["IsActive"],
    );
  }

  static List<Directory> fromJsonList(list) {
    if (list == null) return null;
    return list.map((item) => Directory.fromJson(item)).toList();
  }
}
