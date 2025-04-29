import { Injectable } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs';
import { BsConfirmModalComponent } from './bs-confirm-modal/bs-confirm-modal.component';

@Injectable({
  providedIn: 'root'
})
export class ConfirmService {


  bsModalRef: BsModalRef = new BsModalRef();

  constructor(public modalService: BsModalService) { }

  confirm(title = "Confirmation",
    message = "Are you sure you want to do this?",
    btnOkText = "OK",
    btnCancelText = "Cancel"): Observable<boolean> {

    const config = {
      class: 'modal-sm',
      initialState: {
        title,
        message,
        btnOkText,
        btnCancelText
      }
    }

    this.bsModalRef = this.modalService.show(BsConfirmModalComponent, config);

    return new Observable<boolean>(this.getResult());
  }





  private getResult() {
    return (observer: { next: (arg0: any) => void; complete: () => void; }) => {
      if (this.bsModalRef.onHidden) {
        const subcription = this.bsModalRef.onHidden.subscribe(() => {
          observer.next(this.bsModalRef.content.result)
          observer.complete();
        });

        return {
          unsubscribe() {
            subcription.unsubscribe();
          }
        }
      }
    }

  }

}
