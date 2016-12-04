CXX_FLAGS   := -I include -std=c++11
LD_FLAGS    := -L lib -lprotobuf -lzmq -lpthread

BIN         := temp-sensor
BIN_STATIC  := temp-sensor-static

SOURCES     := $(wildcard *.cpp)
OBJS        := $(SOURCES:.cpp=.o)

.PHONY: proto

all: $(BIN_STATIC)

$(BIN): proto $(OBJS)
	$(CXX) -o $(BIN) obj/*.o obj/proto/*.o $(LD_FLAGS) 
	strip $(BIN)

$(BIN_STATIC): proto $(OBJS)
	$(CXX) -o $(BIN_STATIC) obj/*.o obj/proto/*.o $(LD_FLAGS) -static
	strip $(BIN_STATIC)

proto:
	make -C proto

clean:
	make -C proto clean
	rm -f obj/*
	rm -f $(BIN)
	rm -f $(BIN_STATIC)

%.o: %.cpp
	$(CXX) $(CXX_FLAGS) -c $< -o obj/$@
