import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

void main() => runApp(MyApp());

class MyApp extends StatelessWidget {
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
        title: 'Billys Todo List',
        theme: ThemeData(
          // This is the theme of your application.
          //
          // Try running your application with "flutter run". You'll see the
          // application has a blue toolbar. Then, without quitting the app, try
          // changing the primarySwatch below to Colors.green and then invoke
          // "hot reload" (press "r" in the console where you ran "flutter run",
          // or simply save your changes to "hot reload" in a Flutter IDE).
          // Notice that the counter didn't reset back to zero; the application
          // is not restarted.
          primarySwatch: Colors.blue,
        ),
        home: TodoList());
  }
}

class TodoList extends StatefulWidget {
  @override
  State<StatefulWidget> createState() {
    return new TodoListState();
  }
}

class TodoItem {
  final String id;
  final String text;

  TodoItem({this.id, this.text});

  factory TodoItem.fromJson(Map<String, dynamic> json) {
    return TodoItem(id: json['Id'], text: json['Text']);
  }

  Map<String, dynamic> toJson() => {    
    'Text': this.text
  };
}

class TodoListState extends State<TodoList> {
  List<TodoItem> _items = [];

  Future<List<TodoItem>> _fetchItems() async {
    final res = await http.get("http://localhost:5033/api/todo");
    if (res.statusCode == 200) {
      Iterable jsonList = json.decode(res.body);
      print(jsonList);
      return jsonList.map((todo) => TodoItem.fromJson(todo)).toList();
    } else {
      throw Exception();
    }
  }

  Future<void> _addItem(String value) async {
    if(value == null) return;
    var todo = new TodoItem(text: value);
    var result = await http.post("http://localhost:5033/api/todo", body: jsonEncode(todo), );
    if (result.statusCode == 200) {
      var items = await _fetchItems();
      setState(() {
        _items = items;
      });
    }
    else {
      throw new Exception();
    }
  }

  Widget _buildTodoList() {
    return new ListView.builder(itemBuilder: (context, index) {
      if (index < _items.length) {
        if(_items[index] != null) {
          return _buildItem(_items[index]);
        }        
      }
    });
  }

  Widget _buildItem(TodoItem todoItem) {
    return new ListTile(title: new Text(todoItem.text ?? "text"));
  }

  Future<void> _addWithScreen() {
    Navigator.of(context).push(new MaterialPageRoute(builder: (context) {
      return new Scaffold(
        appBar: new AppBar(title: new Text("Add a item")),
        body: new TextField(
          autofocus: true,
          onSubmitted: (value) {
            _addItem(value);
            Navigator.pop(context);
          },
          decoration: new InputDecoration(
              hintText: "Enter a task",
              contentPadding: const EdgeInsets.all(16.0)),
        ),
      );
    }));
  }

  @override
  Widget build(BuildContext context) {
    return new Scaffold(
      appBar: new AppBar(title: new Text("Todo List App")),
      body: _buildTodoList(),
      floatingActionButton: new FloatingActionButton(
        onPressed: _addWithScreen,
        tooltip: "Add Item",
        child: Icon(Icons.add),
      ),
    );
  }
}
