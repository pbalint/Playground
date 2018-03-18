import { DialogHostDirective } from '../../directives/dialog-host.directive';
import { DialogService } from '../../services/dialog.service';
import { Component, Input, OnInit, ViewChild, ComponentFactoryResolver, OnDestroy, Type } from '@angular/core';

@Component( {
    selector: 'app-dialog-host',
    templateUrl: './dialog-host.component.html',
    styleUrls: [ './dialog-host.component.css' ]
} )
export class DialogHostComponent implements OnInit, OnDestroy {
    @ViewChild( DialogHostDirective ) adHost: DialogHostDirective;

    constructor( private componentFactoryResolver: ComponentFactoryResolver,
                 private dialogService: DialogService ) { }

    ngOnInit() {
        this.dialogService.registerHost( this );
    }

    ngOnDestroy() {
    }

    public openDialog<T>( componentType: Type<T>, parameterSetter: (dialog: T) => void ): void {
        const componentFactory = this.componentFactoryResolver.resolveComponentFactory( componentType );

        const viewContainerRef = this.adHost.viewContainerRef;
        viewContainerRef.clear();

        const componentRef = viewContainerRef.createComponent( componentFactory );
        if (parameterSetter !== undefined && parameterSetter != null) {
            parameterSetter(componentRef.instance);
        }
    }
}
