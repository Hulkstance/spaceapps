import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { StyleTransferService } from 'src/app/core/services/style-transfer.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  form: FormGroup;

  private componentDestroyed$ = new Subject<boolean>();

  constructor(
    private formBuilder: FormBuilder,
    private styleTransferService: StyleTransferService) {
    this.form = this.initializeForm();
  }

  ngOnDestroy() {
    this.componentDestroyed$.next(true);
    this.componentDestroyed$.complete();
  }

  private initializeForm() {
    return this.formBuilder.group({
      contentUrl: [null, [Validators.required]],
      styleUrl: [null, [Validators.required]]
    });
  }

  onSubmit() {
    if (this.form.invalid) {
      return;
    }

    this.styleTransferService.transfer(this.form.value)
      .pipe(takeUntil(this.componentDestroyed$))
      .subscribe(result => console.log(result));
  }
}
