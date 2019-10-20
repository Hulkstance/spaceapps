import { Component, OnDestroy, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { takeUntil, finalize } from 'rxjs/operators';

import { StyleTransferService } from 'src/app/core/services/style-transfer.service';

@Component({
  selector: 'app-transfer-url',
  templateUrl: './transfer-from-url.component.html',
  styleUrls: ['./transfer-from-url.component.css']
})
export class TransferFromUrlComponent implements OnDestroy {
  @Output() loading = new EventEmitter<boolean>();
  @Output() outputFile = new EventEmitter<string>();

  form: FormGroup;

  private componentDestroyed$ = new Subject<boolean>();

  constructor(
    private formBuilder: FormBuilder,
    private styleTransferService: StyleTransferService) {
    this.loading.emit(false);
    this.form = this.initializeForm();
  }

  private initializeForm() {
    return this.formBuilder.group({
      contentUrl: [null, [Validators.required]],
      styleUrl: [null, [Validators.required]]
    });
  }

  ngOnDestroy() {
    this.componentDestroyed$.next(true);
    this.componentDestroyed$.complete();
  }

  generateStyle() {
    this.loading.emit(true);
    this.outputFile.emit();

    this.styleTransferService.getRandomNASAImage()
      .pipe(
        takeUntil(this.componentDestroyed$),
        finalize(() => this.loading.emit(false))
      )
      .subscribe(image => {
        this.form.controls.styleUrl.setValue(image.url);
      });
  }

  onSubmit() {
    if (this.form.invalid) {
      return;
    }

    this.loading.emit(true);
    this.outputFile.emit();

    this.styleTransferService.transferFromUrl(this.form.value)
      .pipe(
        takeUntil(this.componentDestroyed$),
        finalize(() => this.loading.emit(false))
      )
      .subscribe(result => this.outputFile.emit(result.output_url));
  }
}
