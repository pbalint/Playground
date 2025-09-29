#!/bin/bash

set -e
#set -x

#OPENSSL_CNF_PATH=/etc/pki/tls/openssl.cnf
#OPENSSL_CNF_PATH=/etc/ssl/openssl.cnf
OPENSSL_CNF_PATH=/usr/ssl/openssl.cnf # msys2
ROOT_CA=rootCA
CERT_HOST=htpc
PASSPHRASE=pw123423ewe

openssl genrsa -aes256 -out ${ROOT_CA}.key -passout pass:${PASSPHRASE} 4096
openssl req -x509 -new -nodes -extensions v3_ca -subj "/C=HU/O=PB/CN=${ROOT_CA}" -passin pass:${PASSPHRASE} -key ${ROOT_CA}.key -sha256 -days 365 -out ${ROOT_CA}.crt

openssl genrsa -out ${CERT_HOST}.key 2048
openssl req -new -sha256 -key ${CERT_HOST}.key -subj "/C=HU/O=PB/CN=${CERT_HOST}" -reqexts SAN -extensions SAN \
	-config <(cat ${OPENSSL_CNF_PATH} <(printf "[SAN]\nsubjectAltName=DNS:${CERT_HOST},DNS:localhost,IP:127.0.0.1")) \
	-out ${CERT_HOST}.csr
openssl x509 -req -in ${CERT_HOST}.csr -extensions SAN \
	-extfile <(cat ${OPENSSL_CNF_PATH} <(printf "[SAN]\nsubjectAltName=DNS:${CERT_HOST},DNS:localhost,IP:127.0.0.1")) \
	-CA ${ROOT_CA}.crt -passin pass:${PASSPHRASE} -CAkey ${ROOT_CA}.key -CAcreateserial -out ${CERT_HOST}.crt -days 365 -sha256

#crt -> pfx
openssl pkcs12 -export -out ${CERT_HOST}.pfx -inkey ${CERT_HOST}.key -in ${CERT_HOST}.crt -passin pass:${PASSPHRASE} -passout pass:${PASSPHRASE} -name ${CERT_HOST} 

#to add to java cacerts
#keytool -import -trustcacerts -keystore cacerts -storepass changeit -alias "${ROOT_CA}" -file ${ROOT_CA}}.crt
