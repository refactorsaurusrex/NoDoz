package main

import (
	"log"
	"syscall"
	"time"
)

// Execution States
const (
	EsSystemRequired = 0x00000001
	EsContinuous     = 0x80000000
)

var pulseTime = 10 * time.Second

func main() {
	kernel32 := syscall.NewLazyDLL("kernel32.dll")
	setThreadExecStateProc := kernel32.NewProc("SetThreadExecutionState")

	pulse := time.NewTicker(pulseTime)

	log.Println("Starting keep alive poll... (silence)")
	for {
		select {
		case <-pulse.C:
			setThreadExecStateProc.Call(uintptr(EsSystemRequired))
		}
	}
}
