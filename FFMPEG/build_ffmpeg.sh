#!/bin/sh
OUT_DIR="build_out/"
VS_TOOLCHAIN_PATH="/c/Program Files (x86)/Microsoft Visual Studio 14.0/VC/BIN/x86_amd64"
YASM_PATH="/c/Tools/YAsm"
LIB_X264_PATH="C:\\Projects\\libx264"
LIB_X264_PATH_MSYS="/c/Projects/libx264"

export LIB="$LIB;$LIB_X264_PATH"
export PATH="$VS_TOOLCHAIN_PATH":"$YASM_PATH":$PATH

cd libx264
CC=cl ./configure --enable-static --enable-win32thread --disable-cli --prefix=$OUT_DIR
make && make install
cp libx264.lib x264.lib
cd ..

cd ffmpeg
./configure --toolchain=msvc --enable-libx264 --disable-programs --disable-doc --enable-gpl --extra-cflags=-I$LIB_X264_PATH_MSYS --prefix=$OUT_DIR
make && make install
cd ..