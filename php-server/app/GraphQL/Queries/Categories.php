<?php

namespace App\GraphQL\Queries;

use App\Features\Categories\Category;
use Doctrine\ORM\EntityManager;
use GraphQL\Type\Definition\ResolveInfo;
use Nuwave\Lighthouse\Support\Contracts\GraphQLContext;

class Categories
{

    /**
     * @var EntityManager
     */
    private $entityManager;

    public function __construct(EntityManager $entityManager)
    {
        $this->entityManager = $entityManager;
    }

    /**
     * Return a value for the field.
     *
     * @param null $rootValue Usually contains the result returned from the parent field. In this case, it is always `null`.
     * @param mixed[] $args The arguments that were passed into the field.
     * @param \Nuwave\Lighthouse\Support\Contracts\GraphQLContext $context Arbitrary data that is shared between all fields of a single query.
     * @param \GraphQL\Type\Definition\ResolveInfo $resolveInfo Information about the query itself, such as the execution state, the field name, path to the field from the root, and more.
     * @return mixed
     */
    public function __invoke($rootValue, array $args, GraphQLContext $context, ResolveInfo $resolveInfo)
    {
        $fields = $resolveInfo->getFieldSelection(5);

        $qb = $this->entityManager->createQueryBuilder()
            ->select('c')
            ->from(Category::class, 'c');

        if (isset($fields['categoryGroup'])) {
            $qb = $qb->join('c.categoryGroup', 'cg')
            -> addSelect('cg'); // eager load the category group
        }

        $qb = $qb->getQuery()
            ->getArrayResult();

        dd($qb);

        return $qb;
    }
}
