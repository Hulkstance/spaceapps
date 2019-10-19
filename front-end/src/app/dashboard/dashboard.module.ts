import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { DashboardRoutingModule } from './dashboard-routing.module';

import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { TransferFromUrlComponent } from './transfer-from-url/transfer-from-url.component';
import { TransferFromFileComponent } from './transfer-from-file/transfer-from-file.component';

@NgModule({
  declarations: [
    HomeComponent,
    AboutComponent,
    TransferFromUrlComponent,
    TransferFromFileComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    DashboardRoutingModule
  ]
})
export class DashboardModule { }
