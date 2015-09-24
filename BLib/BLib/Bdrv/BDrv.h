#ifdef __cplusplus
namespace BDRV
{
#endif

#define BDRV_IOCTL_CALL CTL_CODE( FILE_DEVICE_UNKNOWN, 0x1024, METHOD_BUFFERED, FILE_ANY_ACCESS )

enum CallType { PortRead1, PortRead2, PortRead4,
                PortWrite1, PortWrite2, PortWrite4,
                PCIReadConfig, PCIWriteConfig, TestCall };

struct Call
{
    enum CallType   type;
    union
    {
        struct 
        {
            unsigned long   address;
            unsigned int    value;
        } port;

        struct
        {
            unsigned long   bus;
            unsigned long   slot;
            unsigned long   offset;
            unsigned int    value;
        } pci_config;
    } params;
};

#ifdef __cplusplus
}
#endif
