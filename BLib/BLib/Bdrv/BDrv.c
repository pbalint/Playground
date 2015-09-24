#include <string.h>
#include <ntddk.h>

#include "BDrv.h"

#define NT_DEVICE_NAME      L"\\Device\\BDrv"
#define DOS_DEVICE_NAME     L"\\DosDevices\\BDrv"

DRIVER_INITIALIZE       DriverEntry;
__drv_dispatchType(IRP_MJ_CREATE)           DRIVER_DISPATCH Create;
__drv_dispatchType(IRP_MJ_CLOSE)            DRIVER_DISPATCH Close;
__drv_dispatchType(IRP_MJ_DEVICE_CONTROL)   DRIVER_DISPATCH Control;
DRIVER_UNLOAD           Unload;

NTSTATUS Create( PDEVICE_OBJECT device_object, PIRP Irp );
NTSTATUS Close( PDEVICE_OBJECT device_object, PIRP Irp );
NTSTATUS Control( PDEVICE_OBJECT device_object, PIRP Irp );
VOID Unload( PDRIVER_OBJECT driver_object );

NTSTATUS CompleteIRP( PIRP irp, NTSTATUS status, unsigned long information )
{
    irp->IoStatus.Status      = status;
    irp->IoStatus.Information = information;
    IoCompleteRequest( irp, IO_NO_INCREMENT );
    return status;
}

NTSTATUS DriverEntry( PDRIVER_OBJECT driver_object, PUNICODE_STRING registry_path )
{
    NTSTATUS        status;
    UNICODE_STRING  device_name;
    UNICODE_STRING  dos_name;
    PDEVICE_OBJECT  device_object = NULL;

    UNREFERENCED_PARAMETER( registry_path );

    RtlInitUnicodeString( &device_name, NT_DEVICE_NAME );
    status = IoCreateDevice( driver_object, 0, &device_name, FILE_DEVICE_UNKNOWN, FILE_DEVICE_SECURE_OPEN, FALSE, &device_object );
    KdPrint(( "Driver object: %x\n", driver_object ));
    if ( !NT_SUCCESS( status ) )
    {
        KdPrint(( "Couldn't create the device object\n" ));
        return status;
    }
    KdPrint(( "Device object: %x\n", device_object ));

    driver_object->MajorFunction[ IRP_MJ_CREATE ]          = Create;
    driver_object->MajorFunction[ IRP_MJ_CLOSE ]           = Close;
    driver_object->MajorFunction[ IRP_MJ_DEVICE_CONTROL ]  = Control;
    driver_object->DriverUnload                            = Unload;

    RtlInitUnicodeString( &dos_name, DOS_DEVICE_NAME );
    status = IoCreateSymbolicLink( &dos_name, &device_name );
    if ( !NT_SUCCESS( status ) )
    {
        KdPrint(("Couldn't create symbolic link\n"));
        IoDeleteDevice( device_object );
    }

    return status;
}


NTSTATUS Create( PDEVICE_OBJECT device_object, PIRP Irp )
{
    UNREFERENCED_PARAMETER( device_object );
    return CompleteIRP( Irp, STATUS_SUCCESS, 0 );
}

NTSTATUS Close( PDEVICE_OBJECT device_object, PIRP Irp )
{
    UNREFERENCED_PARAMETER( device_object );
    return CompleteIRP( Irp, STATUS_SUCCESS, 0 );
}

VOID Unload( PDRIVER_OBJECT driver_object )
{
    UNICODE_STRING uniWin32NameString;

    RtlInitUnicodeString( &uniWin32NameString, DOS_DEVICE_NAME );
    IoDeleteSymbolicLink( &uniWin32NameString );
    IoDeleteDevice( driver_object->DeviceObject );
}

NTSTATUS Control( PDEVICE_OBJECT device_object, PIRP irp )
{
    PIO_STACK_LOCATION  irp_sp = IoGetCurrentIrpStackLocation( irp );

    UNREFERENCED_PARAMETER( device_object );

    if ( irp_sp->Parameters.DeviceIoControl.InputBufferLength  < sizeof( struct Call ) ||
         irp_sp->Parameters.DeviceIoControl.OutputBufferLength < sizeof( struct Call ) )
    {
        return CompleteIRP( irp, STATUS_INVALID_PARAMETER, 0 );
    }

    switch ( irp_sp->Parameters.DeviceIoControl.IoControlCode )
    {
        case BDRV_IOCTL_CALL:
        {
            struct Call* call = irp->AssociatedIrp.SystemBuffer;
            switch ( call->type )
            {
                case PortRead1:
                    call->params.port.value = READ_PORT_UCHAR( (PUCHAR)call->params.port.address ); 
                    break;
                case PortRead2:
                    call->params.port.value = READ_PORT_USHORT( (PUSHORT)call->params.port.address );
                    break;
                case PortRead4:
                    call->params.port.value = READ_PORT_ULONG( (PULONG)call->params.port.address );
                    break;

                case PortWrite1:
                    WRITE_PORT_UCHAR( (PUCHAR)call->params.port.address, (UCHAR)call->params.port.value );
                    break;
                case PortWrite2:
                    WRITE_PORT_USHORT( (PUSHORT)call->params.port.address, (USHORT)call->params.port.value );
                    break;
                case PortWrite4:
                    WRITE_PORT_ULONG( (PULONG)call->params.port.address, (ULONG)call->params.port.value );
                    break;

/*                case PCIReadConfig:
                    ReadWritePciConfig( &call->params.pci_config.bus,
                                        &call->params.pci_config.slot,
                                        &call->params.pci_config.offset,
                                        &call->params.pci_config.value,
                                        sizeof( call->params.port.value ),
                                        TRUE );
                    break;
                case PCIWriteConfig:
                    ReadWritePciConfig( &call->params.pci_config.bus,
                                        &call->params.pci_config.slot,
                                        &call->params.pci_config.offset,
                                        &call->params.pci_config.value,
                                        sizeof( call->params.port.value ),
                                        FALSE );
                                        */
                    break;
            }

            return CompleteIRP( irp, STATUS_SUCCESS, sizeof( struct Call ) );
            break;
        }

        default:
        {
            KdPrint(("ERROR: unrecognized IOCTL %x\n", irp_sp->Parameters.DeviceIoControl.IoControlCode));
            return CompleteIRP( irp, STATUS_INVALID_DEVICE_REQUEST, 0 );
            break;
        }
    }
}
/*
NTSTATUS ReadWritePciConfig( unsigned long* bus,
                             unsigned long* slot,
                             unsigned long* offset,
                             void*          buffer,
                             size_t         length,
                             int            read )
{
    unsigned long result;
    if ( read ) 
    {
        result = HalGetBusDataByOffset( PCIConfiguration, *bus, *slot, buffer, *offset, length );
    }
    else
    {
        result = HalSetBusDataByOffset( PCIConfiguration, *bus, *slot, buffer, *offset, length );
    }

    if ( result == 0 || result == 2 )
    {
        *bus     = 0xffffffff;
        *slot    = 0xffffffff;
        *offset  = 0xffffffff;
    }

    return result;
}
*/