#pragma once

#include "BMPHeader.h"
#include "Image.h"

namespace BLib
{

void CopyBmpData( Image* image, const BMPHeader& bmp_header );
void CopyBmpDataUsingPalette( Image* image, const BMPHeader& bmp_header );

void ShuffleRGBToBGR( Image* image );

void ConvertGrayToRGB(  unsigned char* src_ptr, unsigned char* dst_ptr, unsigned int width, unsigned int height, unsigned int src_padding, unsigned int dst_padding );
void ConvertGrayToRGBA( unsigned char* src_ptr, unsigned char* dst_ptr, unsigned int width, unsigned int height, unsigned int src_padding, unsigned int dst_padding );
void ConvertGrayToYUV(  unsigned char* src_ptr, unsigned char* dst_ptr, unsigned int width, unsigned int height, unsigned int src_padding, unsigned int dst_padding );
void ConvertGrayToYUVX( unsigned char* src_ptr, unsigned char* dst_ptr, unsigned int width, unsigned int height, unsigned int src_padding, unsigned int dst_padding );

void ConvertRGBToGray( unsigned char* src_ptr, unsigned char* dst_ptr, unsigned int width, unsigned int height, unsigned int src_padding, unsigned int dst_padding );
void ConvertRGBToRGBA( unsigned char* src_ptr, unsigned char* dst_ptr, unsigned int width, unsigned int height, unsigned int src_padding, unsigned int dst_padding );
void ConvertRGBToYUV(  unsigned char* src_ptr, unsigned char* dst_ptr, unsigned int width, unsigned int height, unsigned int src_padding, unsigned int dst_padding );
void ConvertRGBToYUVX( unsigned char* src_ptr, unsigned char* dst_ptr, unsigned int width, unsigned int height, unsigned int src_padding, unsigned int dst_padding );

void ConvertRGBAToGray( unsigned char* src_ptr, unsigned char* dst_ptr, unsigned int width, unsigned int height, unsigned int src_padding, unsigned int dst_padding );
void ConvertRGBAToRGB(  unsigned char* src_ptr, unsigned char* dst_ptr, unsigned int width, unsigned int height, unsigned int src_padding, unsigned int dst_padding );
void ConvertRGBAToYUV(  unsigned char* src_ptr, unsigned char* dst_ptr, unsigned int width, unsigned int height, unsigned int src_padding, unsigned int dst_padding );
void ConvertRGBAToYUVX( unsigned char* src_ptr, unsigned char* dst_ptr, unsigned int width, unsigned int height, unsigned int src_padding, unsigned int dst_padding );

void ConvertYUVToGray( unsigned char* src_ptr, unsigned char* dst_ptr, unsigned int width, unsigned int height, unsigned int src_padding, unsigned int dst_padding );
void ConvertYUVToRGB(  unsigned char* src_ptr, unsigned char* dst_ptr, unsigned int width, unsigned int height, unsigned int src_padding, unsigned int dst_padding );
void ConvertYUVToRGBA( unsigned char* src_ptr, unsigned char* dst_ptr, unsigned int width, unsigned int height, unsigned int src_padding, unsigned int dst_padding );
void ConvertYUVToYUVX( unsigned char* src_ptr, unsigned char* dst_ptr, unsigned int width, unsigned int height, unsigned int src_padding, unsigned int dst_padding );

void ConvertYUVXToGray( unsigned char* src_ptr, unsigned char* dst_ptr, unsigned int width, unsigned int height, unsigned int src_padding, unsigned int dst_padding );
void ConvertYUVXToRGB(  unsigned char* src_ptr, unsigned char* dst_ptr, unsigned int width, unsigned int height, unsigned int src_padding, unsigned int dst_padding );
void ConvertYUVXToRGBA( unsigned char* src_ptr, unsigned char* dst_ptr, unsigned int width, unsigned int height, unsigned int src_padding, unsigned int dst_padding );
void ConvertYUVXToYUV(  unsigned char* src_ptr, unsigned char* dst_ptr, unsigned int width, unsigned int height, unsigned int src_padding, unsigned int dst_padding );

}