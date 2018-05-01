CXX_FLAGS   := -O2 -I include/protobuf -I include/zmq -std=c++11
#CXX_FLAGS   := -g -O0 -I include/protobuf -I include/zmq -std=c++11
LD_FLAGS    := -L lib/protobuf -L lib/zmq -lprotobuf -lzmq -lpthread

BIN         := temp-sensor
BIN_STATIC  := temp-sensor-static

SOURCES     := $(wildcard *.cpp)
OBJS        := $(SOURCES:.cpp=.o)

.PHONY:

all: $(BIN_STATIC)

protos:
	make -C proto

$(BIN): protos $(OBJS)
	$(CXX) -o $(BIN) obj/*.o proto/*.o $(LD_FLAGS) 
	strip $(BIN)

$(BIN_STATIC): protos $(OBJS)
	$(CXX) -o $(BIN_STATIC) obj/*.o proto/*.o $(LD_FLAGS) -static
	strip $(BIN_STATIC)

clean:
	make -C proto clean
	rm -f obj/*
	rm -f $(BIN)
	rm -f $(BIN_STATIC)

%.o: %.cpp
	$(CXX) $(CXX_FLAGS) -c $< -o obj/$@
