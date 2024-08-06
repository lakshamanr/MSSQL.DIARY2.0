import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api/menuitem';

@Component({
  selector: 'app-database-role-details',
  templateUrl: './database-role-details.component.html',
  styleUrls: ['./database-role-details.component.css']
})
export class DatabaseRoleDetailsComponent implements OnInit {

  public items: MenuItem[];
  public home: MenuItem;

  constructor() { }

  ngOnInit() {
  }

}
