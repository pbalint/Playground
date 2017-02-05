package com.pb.repositories;

import org.springframework.data.repository.CrudRepository;

import com.pb.entities.RestLogEntry;

public interface RestLogRepository extends CrudRepository<RestLogEntry, Long> {
/*        Employee findByFirstName(String firstName);

        List<Employee> findByLastName(String lastName);
      }
*/

}
