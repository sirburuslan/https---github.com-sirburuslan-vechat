/*
 * @file User
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-06-23
 *
 * This file contains same funtions used for users
 */

// Object ID Type
type objectId = {
    timestamp: number,
    machine: number,
    pid: number,
    increment: number,
    creationTime: string
};

/**
 * Converts object id to user id
 * 
 * @param objectId objectId
 * 
 * @returns string user id
 */
const userIdFromObjectId = (objectId: objectId): string => {
    const timestampHex = objectId.timestamp.toString(16).padStart(8, '0');
    const machineHex = objectId.machine.toString(16).padStart(6, '0');
    const pidHex = objectId.pid.toString(16).padStart(4, '0');
    const incrementHex = objectId.increment.toString(16).padStart(6, '0');
    return (timestampHex + machineHex + pidHex + incrementHex);
}
  
// Export the functions
export { 
    userIdFromObjectId
};