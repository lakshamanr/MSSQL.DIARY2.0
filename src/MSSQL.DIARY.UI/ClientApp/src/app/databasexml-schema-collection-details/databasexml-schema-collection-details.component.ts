import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api/menuitem';

@Component({
  selector: 'app-databasexml-schema-collection-details',
  templateUrl: './databasexml-schema-collection-details.component.html',
  styleUrls: ['./databasexml-schema-collection-details.component.css']
})
export class DatabasexmlSchemaCollectionDetailsComponent implements OnInit {

  public items: MenuItem[];
  public home: MenuItem;

  constructor() { }

  ngOnInit() {
  }

}
