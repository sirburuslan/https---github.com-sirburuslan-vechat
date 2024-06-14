/*
 * @file Sanitize
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-04
 *
 * This file contains same funtions used to sanitize the data
 */

// Installed Utils
import DOMPurify from 'isomorphic-dompurify';

/**
 * Sanitize a string
 * @param input 
 * @returns sanitized string
 */
const sanitizeInput = (input: string): string => {
  return DOMPurify.sanitize(input);
};

// Export the functions
export { sanitizeInput };
