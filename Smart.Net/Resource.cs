﻿namespace Simplified.IO
{
    public sealed class Resource
    {
        /* Note: storing registers in string for more flexibility should I need to refactor and or change the design. */
        public static string SmartAttributes =
@"0x00,Invalid
0x01,Raw read error rate
0x02,Throughput performance
0x03,Spinup time
0x04,Start/Stop count
0x05,Reallocated sector count
0x06,Read channel margin
0x07,Seek error rate
0x08,Seek timer performance
0x09,Power-on hours count
0x0A,Spinup retry count
0x0B,Calibration retry count
0x0C,Power cycle count
0x0D,Soft read error rate
0xA8,SATA PHY Error Count
0xAA,Available reserved space
0xAB,Program fail count
0xAC,Erase fail block count
0xAD,Wear level count
0xAE,Unexpected power loss count
0xAF,Bad Cluster Table Count
0xB1,Wear Leveling Count
0xB3,Used Reserved Block Count (Total)
0xB5,Program Fail Count (Total)
0xB6,Erase Fail Count (Total)
0xB7,SATA downshift count
0xB8,End-to-End error
0xBB,Uncorrectable error count
0xBC,Command Timeout
0xBD,High Fly Writes
0xBE,Airflow Temperature
0xBF,G-sense error rate
0xC0,Unsafe shutdown count
0xC1,Load/Unload cycle count
0xC2,Temperature
0xC3,Hardware ECC recovered
0xC4,Reallocation count
0xC5,Current pending sector count
0xC6,Offline scan uncorrectable count
0xC7,Interface CRC error rate
0xC8,Write error rate
0xC9,Soft read error rate
0xCA,Data Address Mark errors
0xCB,Run out cancel
0xCC,Soft ECC correction
0xCD,Thermal asperity rate (TAR)
0xCE,Flying height
0xCF,Spin high current
0xD0,Spin buzz
0xD1,Offline seek performance
0xDA,CRC Error Count
0xDC,Disk shift
0xDD,G-sense error rate
0xDE,Loaded hours
0xDF,Load/unload retry count
0xE0,Load friction
0xE1,Host writes
0xE2,Timer workload media wear
0xE3,Timer workload read/write ratio
0xE4,Timer workload timer
0xE6,GMR head amplitude
0xE7,Temperature / SSD Life Left
0xE8,Available reserved space
0xE9,Media wearout indicator
0xEB,POR Recovery Count
0xF0,Head flying hours
0xF1,Life time writes
0xF2,Life time reads
0xF9,Life time writes (NAND)
0xFA,Read error retry rate";
    }
}
