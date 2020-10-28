const objs = [
    { a: 'a', b: 'ab' },
    { b: 'b' },
    { c: 'c', b: 'cb' }
  ];
const collection = (a, e) => a.concat([e]);
const a = objs.reduce(collection, []);
console.log( 
  'collection aggregation',
  a,
  a[1].b,
  a[2].c,
  `enumerable keys: ${ Object.keys(a) }`
);