import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

import { StyleTransfer } from '../types/style-transfer';

@Injectable({
  providedIn: 'root'
})
export class StyleTransferService {
  private actionUrl: string;

  constructor(private httpClient: HttpClient) {
    this.actionUrl = `${environment.baseUrls.server}api/styletransfer`;
  }

  getRandomNASAImage() {
    return this.httpClient.get<any>(this.actionUrl);
  }

  transferFromUrl(styleTransfer: StyleTransfer) {
    return this.httpClient.post<StyleTransfer>(`${this.actionUrl}/urltransfer`, styleTransfer);
  }

  transferFromFile(formData: FormData) {
    return this.httpClient.post<StyleTransfer>(`${this.actionUrl}/filetransfer`, formData);
  }
}
