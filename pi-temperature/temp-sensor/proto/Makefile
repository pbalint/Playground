CXX_FLAGS += -I../include -std=c++11

CPP_DIR         := cpp
JAVA_DIR        := java
OBJ_DIR         := ../obj/proto
DIRS            := ${CPP_DIR} ${JAVA_DIR} ${OBJ_DIR}
PROTO_OBJS      := $(addprefix ${OBJ_DIR}/, $(patsubst %.proto,%.pb.o,$(wildcard *.proto)))

.PRECIOUS: ${CPP_DIR}/%.pb.cc 

all: ${DIRS} $(PROTO_OBJS)

${DIRS}:
	mkdir -p ${DIRS}

clean:
	rm -rf ${DIRS}

${OBJ_DIR}/%.pb.o: ${CPP_DIR}/%.pb.cc
	$(CXX) $(CXX_FLAGS) -c $< -o $@

${CPP_DIR}/%.pb.cc: %.proto
	./protoc --cpp_out=${CPP_DIR} $<
	./protoc --java_out=${JAVA_DIR} $<