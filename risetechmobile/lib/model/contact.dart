class Contact {
  int id;
  String personId;
  String telephone;
  String email;
  String location;
  bool isActive;

  Contact({
    this.id,
    this.personId,
    this.telephone,
    this.email,
    this.location,
    this.isActive,
  });

  factory Contact.fromJson(Map<String, dynamic> json) {
    return Contact(
      id: json["Id"],
      personId: json["PersonId"],
      telephone: json["Telephone"],
      email: json["Email"],
      location: json["Location"],
      isActive: json["IsActive"],
    );
  }

  static List<Contact> fromJsonList(list) {
    if (list == null) return null;
    return list.map((item) => Contact.fromJson(item)).toList();
  }
}
