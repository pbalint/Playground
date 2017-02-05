create table rest_log (
    id 						bigserial PRIMARY KEY,
    time_stamp				bigint not null,
    host_name 				text,
    ip_address 				text not null,
    url 					text not null,
    method 					text not null,
    rtt_ms					integer not null,
    request_payload 		text,
    request_payload_size 	integer not null,
    response_code 			integer not null,
    response_payload 		text,
    response_payload_size 	integer not null
);

create index idx_rest_log_host_name 	ON rest_log (host_name);
create index idx_rest_log_ip_address 	ON rest_log (ip_address);
create index idx_rest_log_method 		ON rest_log (method);
create index idx_rest_log_response_code ON rest_log (response_code);
