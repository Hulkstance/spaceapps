import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { StyleTransferService } from './style-transfer.service';

describe('StyleTransferService', () => {
  let httpTestingController: HttpTestingController;
  let service: StyleTransferService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [StyleTransferService],
      imports: [HttpClientTestingModule]
    });

    httpTestingController = TestBed.get(HttpTestingController);
    service = TestBed.get(StyleTransferService);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});