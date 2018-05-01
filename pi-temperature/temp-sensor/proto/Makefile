CXX_FLAGS += -I ../include/protobuf -std=c++11

PROTOS         := $(wildcard *.proto)
PROTO_OBJS     := $(PROTOS:.proto=.pb.o)

all: $(PROTO_OBJS)

clean: 
	rm -f *.pb.o
	rm -f *.pb.h
	rm -f *.pb.cc
	rm -rf com

%.pb.o: %.pb.cc
	$(CXX) $(CXX_FLAGS) -c $< -o $@

%.pb.cc: %.proto
	LD_LIBRARY_PATH=. && ./protoc --cpp_out=. $<
	LD_LIBRARY_PATH=. && ./protoc --java_out=. $<