#CXX_FLAGS   := -O2 -std=c++11
CXX_FLAGS   := -g -O0 -std=c++11
LD_FLAGS    := -lpthread

BIN         := tele-socket
BIN_STATIC  := tele-socket-static

SOURCES     := $(wildcard *.cpp) $(wildcard Connection/*.cpp)
OBJS        := $(SOURCES:.cpp=.o)

.PHONY:

all: $(BIN_STATIC)

dirs:
	mkdir -p obj/Connection

$(BIN): dirs $(OBJS)
	$(CXX) -o $(BIN) obj/*.o obj/Connection/*.o $(LD_FLAGS) 
	strip $(BIN)

$(BIN_STATIC): dirs $(OBJS)
	$(CXX) -o $(BIN_STATIC) obj/*.o obj/Connection/*.o $(LD_FLAGS) -static
	strip $(BIN_STATIC)

clean:
	rm -rf obj
	rm -f $(BIN)
	rm -f $(BIN_STATIC)

%.o: %.cpp
	$(CXX) $(CXX_FLAGS) -c $< -o obj/$@