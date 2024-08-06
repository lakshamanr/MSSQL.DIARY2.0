import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api/menuitem';

@Component({
  selector: 'app-databasexml-schema-collections',
  templateUrl: './databasexml-schema-collections.component.html',
  styleUrls: ['./databasexml-schema-collections.component.css']
})
export class DatabasexmlSchemaCollectionsComponent implements OnInit {

  public items: MenuItem[];
  public home: MenuItem;

  constructor() { }

  ngOnInit() {
  }

}
