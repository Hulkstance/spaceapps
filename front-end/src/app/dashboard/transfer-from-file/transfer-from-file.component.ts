import { Component, OnDestroy, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { takeUntil, finalize } from 'rxjs/operators';

import { StyleTransferService } from 'src/app/core/services/style-transfer.service';

@Component({
  selector: 'app-transfer-file',
  templateUrl: './transfer-from-file.component.html',
  styleUrls: ['./transfer-from-file.component.css']
})
export class TransferFromFileComponent implements OnDestroy {
  @Output() loading = new EventEmitter<boolean>();
  @Output() outputFile = new EventEmitter<string>();

  form: FormGroup;
  contentData: File;
  styleData: File;

  private componentDestroyed$ = new Subject<boolean>();

  constructor(
    private formBuilder: FormBuilder,
    private styleTransferService: StyleTransferService) {
    this.loading.emit(false);
    this.form = this.initializeForm();
  }

  private initializeForm() {
    return this.formBuilder.group({
      content: [null, [Validators.required]],
      style: [null, [Validators.required]]
    });
  }

  ngOnDestroy() {
    this.componentDestroyed$.next(true);
    this.componentDestroyed$.complete();
  }

  onContentFileChanged(event: any) {
    this.contentData = event.target.files[0];
  }

  onStyleFileChanged(event: any) {
    this.styleData = event.target.files[0];
  }

  onSubmit() {
    if (this.form.invalid) {
      return;
    }

    this.loading.emit(true);

    const uploadData = new FormData();
    uploadData.append('content', this.contentData, this.contentData.name);
    uploadData.append('style', this.styleData, this.styleData.name);

    this.styleTransferService.transferFromFile(uploadData)
      .pipe(
        takeUntil(this.componentDestroyed$),
        finalize(() => this.loading.emit(false))
      )
      .subscribe(result => this.outputFile.emit(result.output_url));
  }
}
