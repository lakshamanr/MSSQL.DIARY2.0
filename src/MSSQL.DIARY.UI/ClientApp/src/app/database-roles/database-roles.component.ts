import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api/menuitem';

@Component({
  selector: 'app-database-roles',
  templateUrl: './database-roles.component.html',
  styleUrls: ['./database-roles.component.css']
})
export class DatabaseRolesComponent implements OnInit {

  public items: MenuItem[];
  public home: MenuItem;

  constructor() { }

  ngOnInit() {
  }

}
