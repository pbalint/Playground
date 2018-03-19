import { DialogHostComponent } from '../components/dialog-host/dialog-host.component';
import { Injectable, Type } from '@angular/core';

@Injectable()
export class DialogService {
    private dialogHost: DialogHostComponent;

    constructor() { }

    public registerHost( dialogHost: DialogHostComponent ): void {
        this.dialogHost = dialogHost;
    }

    public openDialog<T>( componentType: Type<T>, parameterSetter: (dialog: T) => void ) {
        this.dialogHost.openDialog( componentType, parameterSetter );
    }

    public closeDialog(): void {
        this.dialogHost.closeDialog();
    }
}
